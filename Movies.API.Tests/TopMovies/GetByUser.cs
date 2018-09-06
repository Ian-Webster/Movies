using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Domain.DTO;
using NUnit.Framework;
using System.Collections.Generic;

namespace Movies.API.Tests.TopMovies
{
    [TestFixture]
    public class GetByUser: TopMoviesBase
    {

        [TestCase(0)]
        [TestCase(-1)]
        public void Should_ReturnBadRequest_WhenUserIdInvalid(int userId)
        {
            //arrange/act
            var result = GetController().Get(userId);

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void Should_ReturnRequest_WhenUserDoesNotExist()
        {
            //arrange
            MockUserService.Setup(s => s.UserExists(It.IsAny<int>())).Returns(false);

            //act
            var result = GetController().Get(1);

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void Should_CallCorrectServiceMethod()
        {
            //arrange/act
            GetController().Get(1);

            //assert
            MockMovieService.Verify(s => s.TopMoviesByUser(It.IsAny<byte>(), It.IsAny<int>()), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnNotFound_IfMoviesAreNullOrEmpty(bool isNull)
        {
            //arrange
            if (isNull)
            {
                MockMovieService.Setup(s => s.TopMoviesByUser(It.IsAny<byte>(), It.IsAny<int>())).Returns(null as List<Movie>);
            }
            else
            {
                MockMovieService.Setup(s => s.TopMoviesByUser(It.IsAny<byte>(), It.IsAny<int>())).Returns(new List<Movie>());
            }

            //act
            var result = GetController().Get(1);

            //assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Should_ReturnJsonResult()
        {
            //arrange
            MockMovieService.Setup(s => s.TopMoviesByUser(It.IsAny<byte>(), It.IsAny<int>())).Returns(new List<Movie> { new Movie() });

            //act
            var result = GetController().Get(1);

            //assert
            Assert.IsInstanceOf<JsonResult>(result);
        }
    }
}
