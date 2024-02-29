using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using dto = Movies.Domain.DTO;

namespace Movies.API.Tests.SearchMovies
{
    [TestFixture]
    public class Get : SearchMoviesBase
    {

        [TestCase(MovieSearchValidationResults.InvalidCriteria)]
        [TestCase(MovieSearchValidationResults.NoCriteria)]
        public void Should_ReturnExpectedResponseWhen_Invalid(MovieSearchValidationResults validationResult)
        {
            //arrange
            MockMovieService.Setup(s => s.ValidateSearchCriteria(It.IsAny<MovieSearchCriteria>())).Returns(validationResult);

            //act
            var asycResult = GetController().Get(new MovieSearchCriteria());

            //assert
            var result = asycResult.Result;
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
        public void Should_ReturnNotFoundWhen_ResultIsNullOrEmpty(bool isNull)
        {
            //arrange
            MockMovieService.Setup(s => s.ValidateSearchCriteria(It.IsAny<MovieSearchCriteria>())).Returns(MovieSearchValidationResults.OK);
            if (isNull)
            {
                MockMovieService.Setup(s => s.SearchMoviesAsync(It.IsAny<MovieSearchCriteria>())).Returns(Task.FromResult(null as List<dto.Movie>));
            }
            else
            {
                MockMovieService.Setup(s => s.SearchMoviesAsync(It.IsAny<MovieSearchCriteria>())).Returns(Task.FromResult(new List<dto.Movie>()));
            }

            //act
            var asyncResult = GetController().Get(new MovieSearchCriteria());

            //assert
            var result = asyncResult.Result;
            Assert.That(result, Is.InstanceOf<NotFoundResult>());

        }

        [Test]
        public void Should_ReturnJson()
        {
            //arrange
            MockMovieService.Setup(s => s.ValidateSearchCriteria(It.IsAny<MovieSearchCriteria>())).Returns(MovieSearchValidationResults.OK);
            MockMovieService.Setup(s => s.SearchMoviesAsync(It.IsAny<MovieSearchCriteria>())).Returns(Task.FromResult(new List<dto.Movie> { new dto.Movie()}));

            //act
            var jsonAsyncResult = GetController().Get(new MovieSearchCriteria());

            //assert
            var jsonResult = jsonAsyncResult.Result;
            Assert.That(jsonResult, Is.InstanceOf<JsonResult>());
        }
    }
}
