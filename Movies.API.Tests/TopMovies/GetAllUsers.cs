using Microsoft.AspNetCore.Mvc;
using Moq;
using dto = Movies.Domain.DTO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Movies.API.Tests.TopMovies
{
    [TestFixture]
    public class GetAllUsers : TopMoviesBase
    {

        [Test]
        public async Task Should_CallCorrectServiceMethodAsync()
        {
            // Arrange / Act
            await GetController().Get();

            // Assert
            MockMovieService.Verify(s => s.TopMovies(It.IsAny<byte>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task Should_ReturnNotFound_IfMoviesAreNullOrEmpty(bool isNull)
        {
            // Arrange
            if (isNull)
            {
                MockMovieService.Setup(s => s.TopMovies(It.IsAny<byte>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(null as IEnumerable<dto.Movie>));
            }
            else
            {
                MockMovieService.Setup(s => s.TopMovies(It.IsAny<byte>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult((IEnumerable<dto.Movie>)new List<dto.Movie>()));
            }

            // Act
            var result = await GetController().Get();

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Should_ReturnJsonResult()
        {
            // Arrange
            MockMovieService.Setup(s => s.TopMovies(It.IsAny<byte>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((IEnumerable<dto.Movie>)new List<dto.Movie> { new dto.Movie() }));

            // Act
            var result = await GetController().Get();

            // Assert
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

    }
}
