using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Domain.DTO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var asyncResult = GetController().Get(userId);

            //assert
            var result = asyncResult.Result;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void Should_ReturnRequest_WhenUserDoesNotExist()
        {
            //arrange
            MockUserService.Setup(s => s.UserExistsAsync(It.IsAny<int>())).Returns(Task.FromResult(false));

            //act
            var asyncResult = GetController().Get(1);

            //assert
            var result = asyncResult.Result;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Should_CallCorrectServiceMethodAsync()
        {
            //arrange/act
            await GetController().Get(1);

            //assert
            MockMovieService.Verify(s => s.TopMoviesByUserAsync(It.IsAny<byte>(), It.IsAny<int>()), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnNotFound_IfMoviesAreNullOrEmpty(bool isNull)
        {
            //arrange
            if (isNull)
            {
                MockMovieService.Setup(s => s.TopMoviesByUserAsync(It.IsAny<byte>(), It.IsAny<int>())).Returns(Task.FromResult(null as List<Movie>));
            }
            else
            {
                MockMovieService.Setup(s => s.TopMoviesByUserAsync(It.IsAny<byte>(), It.IsAny<int>())).Returns(Task.FromResult(new List<Movie>()));
            }

            //act
            var asyncResult = GetController().Get(1);

            //assert
            var result = asyncResult.Result;
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Should_ReturnJsonResult()
        {
            //arrange
            MockMovieService.Setup(s => s.TopMoviesByUserAsync(It.IsAny<byte>(), It.IsAny<int>())).Returns(Task.FromResult(new List<Movie> { new Movie() }));

            //act
            var asyncResult = GetController().Get(1);

            //assert
            var result = asyncResult.Result;
            Assert.IsInstanceOf<JsonResult>(result);
        }
    }
}
