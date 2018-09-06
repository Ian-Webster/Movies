using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Domain.DTO;
using NUnit.Framework;
using System.Collections.Generic;

namespace Movies.API.Tests.TopMovies
{
    [TestFixture]
    public class GetAllUsers : TopMoviesBase
    {

        [Test]
        public void Should_CallCorrectServiceMethod()
        {
            //arrange/act
            GetController().Get();

            //assert
            MockMovieService.Verify(s => s.TopMovies(It.IsAny<byte>()), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnNotFound_IfMoviesAreNullOrEmpty(bool isNull)
        {
            //arrange
            if (isNull)
            {
                MockMovieService.Setup(s => s.TopMovies(It.IsAny<byte>())).Returns(null as List<Movie>);
            }
            else
            {
                MockMovieService.Setup(s => s.TopMovies(It.IsAny<byte>())).Returns(new List<Movie>());
            }

            //act
            var result = GetController().Get();

            //assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Should_ReturnJsonResult()
        {
            //arrange
            MockMovieService.Setup(s => s.TopMovies(It.IsAny<byte>())).Returns(new List<Movie> { new Movie() });

            //act
            var result = GetController().Get();

            //assert
            Assert.IsInstanceOf<JsonResult>(result);
        }

    }
}
