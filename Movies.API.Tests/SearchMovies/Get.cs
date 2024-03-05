using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using dto = Movies.Domain.DTO;

namespace Movies.API.Tests.SearchMovies
{
    [TestFixture]
    public class Get : SearchMoviesBase
    {

        [TestCase(MovieSearchValidationResults.InvalidCriteria)]
        [TestCase(MovieSearchValidationResults.NoCriteria)]
        public async Task Should_ReturnExpectedResponseWhen_Invalid(MovieSearchValidationResults validationResult)
        {
            // Arrange
            MockMovieService.Setup(s => s.ValidateSearchCriteria(It.IsAny<MovieSearchCriteria>())).Returns(validationResult);

            // Act
            var result = await GetController().Get(new MovieSearchCriteria());

            // Assert
            switch (validationResult)
            {
                case MovieSearchValidationResults.InvalidCriteria:
                case MovieSearchValidationResults.NoCriteria:
                    Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
                    var badRequest = result as BadRequestObjectResult;
                    Assert.That(validationResult.ToString(), Is.EqualTo(badRequest.Value));
                    break;
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Should_ReturnNotFoundWhen_ResultIsNullOrEmpty(bool isNull)
        {
            // Arrange
            MockMovieService.Setup(s => s.ValidateSearchCriteria(It.IsAny<MovieSearchCriteria>())).Returns(MovieSearchValidationResults.OK);
            if (isNull)
            {
                MockMovieService
                    .Setup(s => s.SearchMovies(It.IsAny<MovieSearchCriteria>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(null as IEnumerable<dto.Movie>));
            }
            else
            {
                MockMovieService
                    .Setup(s => s.SearchMovies(It.IsAny<MovieSearchCriteria>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Enumerable.Empty<dto.Movie>()));
            }

            // Act
            var asyncResult = GetController().Get(new MovieSearchCriteria());

            // Assert
            var result = asyncResult.Result;
            Assert.That(result, Is.InstanceOf<NotFoundResult>());

        }

        [Test]
        public async Task Should_ReturnJson()
        {
            // Arrange
            MockMovieService.Setup(s => s.ValidateSearchCriteria(It.IsAny<MovieSearchCriteria>())).Returns(MovieSearchValidationResults.OK);
            MockMovieService.Setup(s => s.SearchMovies(It.IsAny<MovieSearchCriteria>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((IEnumerable<dto.Movie>)new List<dto.Movie>() { new dto.Movie() }));

            // Act
            var jsonResult = await GetController().Get(new MovieSearchCriteria());

            // Assert
            Assert.That(jsonResult, Is.InstanceOf<JsonResult>());
        }
    }
}
