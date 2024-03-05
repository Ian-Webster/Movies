using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using dto = Movies.Domain.DTO;

namespace Movies.API.Tests.Movie
{
    [TestFixture]
    public class Post: MovieBase
    {

        [TestCase(true)]
        [TestCase(false)]
        public async Task Should_ReturnOKStatus_WhenMovieIsSaved(bool isSaved)
        {
            // Arrange
            MockMovieService.Setup(s => s.SaveMovie(It.IsAny<dto.Movie>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(isSaved));

            // Act
            var result = await GetController().Post(new dto.Movie());

            // Assert
            if (isSaved)
            {
                Assert.That(result, Is.InstanceOf<OkResult>());
            }
            else
            {
                Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            }
        }

    }
}
