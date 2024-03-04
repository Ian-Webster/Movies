using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class TopMoviesByUser : MovieServiceBase
    {
        [Test]
        public void Should_ThrowException_MovieCountIsZero()
        {
            // Arrange / Act / Assert
            Assert.That(() => GetService().TopMoviesByUserAsync(0, Guid.NewGuid()), Throws.ArgumentException);
        }

        [Test]
        public void Should_ThrowException_WhenUserIdIsEmpty()
        {
            // Arrange / Act / Assert
            Assert.That(() => GetService().TopMoviesByUserAsync(1, Guid.Empty), Throws.ArgumentException);
        }

        [Test]
        public void Should_ThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            MockUserService.Setup(s => s.UserExistsAsync(It.IsAny<Guid>())).Returns(Task.FromResult(false));

            // Act / Assert
            Assert.That(() => GetService().TopMoviesByUserAsync(1, Guid.NewGuid()), Throws.ArgumentException);
        }

        [TestCase((byte)1, "27F271EA-D108-4CCE-B92A-C45050FB7CA1")]
        [TestCase((byte)2, "43DF3E5C-F354-44F2-A321-D130205F2FE7")]
        [TestCase((byte)3, "5FA57BC8-F28D-4486-8E92-50C659F9AEE0")]
        public async Task Should_Call_ExpectedRepositoryMethods_WithExpectedData_WhenInputIsValid(byte movieCount, string userIdString)
        {
            // Arrange
            var userId = Guid.Parse(userIdString);
            MockUserService.Setup(s => s.UserExistsAsync(userId)).Returns(Task.FromResult(true));

            // Act
             await GetService().TopMoviesByUserAsync(movieCount, userId);

            // Assert
            MockUserService.Verify(s => s.UserExistsAsync(userId), Times.Once);
            MockMovieRepository.Verify(s => s.TopMoviesByUserAsync(movieCount, userId), Times.Once);
        }

    }
}
