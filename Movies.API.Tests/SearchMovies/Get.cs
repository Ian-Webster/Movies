using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;

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
            var result = GetController().Get(new MovieSearchCriteria());

            //assert
            switch (validationResult)
            {
                case MovieSearchValidationResults.InvalidCriteria:
                case MovieSearchValidationResults.NoCriteria:
                    Assert.IsInstanceOf<BadRequestObjectResult>(result);
                    var badRequest = result as BadRequestObjectResult;
                    Assert.AreEqual(validationResult.ToString(), badRequest.Value);
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
                MockMovieService.Setup(s => s.SearchMovies(It.IsAny<MovieSearchCriteria>())).Returns(null as List<Movie>);
            }
            else
            {
                MockMovieService.Setup(s => s.SearchMovies(It.IsAny<MovieSearchCriteria>())).Returns(new List<Movie>());
            }

            //act
            var result = GetController().Get(new MovieSearchCriteria());

            //assert
            Assert.IsInstanceOf<NotFoundResult>(result);

        }

        [Test]
        public void Should_ReturnJson()
        {
            //arrange
            MockMovieService.Setup(s => s.ValidateSearchCriteria(It.IsAny<MovieSearchCriteria>())).Returns(MovieSearchValidationResults.OK);
            MockMovieService.Setup(s => s.SearchMovies(It.IsAny<MovieSearchCriteria>())).Returns(new List<Movie> { new Movie()});

            //act
            var jsonResult = GetController().Get(new MovieSearchCriteria());

            //assert
            Assert.IsInstanceOf<JsonResult>(jsonResult);
        }
    }
}
