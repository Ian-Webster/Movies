using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using dto = Movies.Domain.DTO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Tests.TopMovies
{
    [TestFixture]
    public class GetByUser: TopMoviesBase
    {

        [Test]
        public void Should_ReturnBadRequest_WhenUserIdInvalid()
        {
            // Arrange / Act
            var asyncResult = GetController().Get(Guid.Empty);

            // Assert
            var result = asyncResult.Result;
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void Should_ReturnBadRequestObject_WhenUserDoesNotExist()
        {
            // Arrange
            MockUserService.Setup(s => s.UserExistsAsync(It.IsAny<Guid>())).Returns(Task.FromResult(false));

            // Act
            var asyncResult = GetController().Get(Guid.NewGuid());

            // Assert
            var result = asyncResult.Result;
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Should_CallCorrectServiceMethodAsync()
        {
            // Arrange / Act
            await GetController().Get(Guid.NewGuid());

            // Assert
            MockMovieService.Verify(s => s.TopMoviesByUserAsync(It.IsAny<byte>(), It.IsAny<Guid>()), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Should_ReturnNotFound_IfMoviesAreNullOrEmpty(bool isNull)
        {
            // Arrange
            if (isNull)
            {
                MockMovieService.Setup(s => s.TopMoviesByUserAsync(It.IsAny<byte>(), It.IsAny<Guid>())).Returns(Task.FromResult(null as List<dto.Movie>));
            }
            else
            {
                MockMovieService.Setup(s => s.TopMoviesByUserAsync(It.IsAny<byte>(), It.IsAny<Guid>())).Returns(Task.FromResult(new List<dto.Movie>()));
            }

            // Act
            var result = await GetController().Get(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Should_ReturnJsonResult()
        {
            // Arrange
            MockMovieService.Setup(s => s.TopMoviesByUserAsync(It.IsAny<byte>(), It.IsAny<Guid>())).Returns(Task.FromResult(new List<dto.Movie> { new dto.Movie() }));

            // Act
            var result = await GetController().Get(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }
    }
}
