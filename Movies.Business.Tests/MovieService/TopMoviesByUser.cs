using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class TopMoviesByUser : MovieServiceBase
    {
        [Test]
        public async Task Should_ThrowException_MovieCountIsZero()
        {
            // Arrange / Act / Assert
            await Assert.ThatAsync(() => GetService().TopMoviesByUser(0, Guid.NewGuid(), GetCancellationToken()), Throws.ArgumentException);
        }

        [Test]
        public async Task Should_ThrowException_WhenUserIdIsEmpty()
        {
            // Arrange / Act / Assert
            await Assert.ThatAsync(() => GetService().TopMoviesByUser(1, Guid.Empty, GetCancellationToken()), Throws.ArgumentException);
        }

        [Test]
        public async Task Should_ThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            MockUserService.Setup(s => s.UserExists(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(false));

            // Act / Assert
            await Assert.ThatAsync(() => GetService().TopMoviesByUser(1, Guid.NewGuid(), GetCancellationToken()), Throws.ArgumentException);
        }

        [TestCase((byte)1, "27F271EA-D108-4CCE-B92A-C45050FB7CA1")]
        [TestCase((byte)2, "43DF3E5C-F354-44F2-A321-D130205F2FE7")]
        [TestCase((byte)3, "5FA57BC8-F28D-4486-8E92-50C659F9AEE0")]
        public async Task Should_Call_ExpectedRepositoryMethods_WithExpectedData_WhenInputIsValid(byte movieCount, string userIdString)
        {
            // Arrange
            var userId = Guid.Parse(userIdString);
            MockUserService.Setup(s => s.UserExists(userId, It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            // Act
             await GetService().TopMoviesByUser(movieCount, userId, GetCancellationToken());

            // Assert
            MockUserService.Verify(s => s.UserExists(userId, It.IsAny<CancellationToken>()), Times.Once);
            MockMovieRepository.Verify(s => s.TopMoviesByUser(movieCount, userId, It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
