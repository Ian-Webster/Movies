using System;
using System.Threading;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Movies.Business.Tests.UserService
{
    [TestFixture]
    public class UserExists: UserServiceBase
    {

        [Test]
        public async Task Should_ThrowException_WhenUserIdIsInvalid()
        {
            // Arrange / Act / Assert
            await Assert.ThatAsync(() => GetService().UserExists(Guid.Empty, GetCancellationToken()), Throws.ArgumentException);
        }

        [TestCase("3763B7C5-992F-4691-8C64-88A70EC11550")]
        [TestCase("B216DDE8-EC63-41C8-B5E1-91430F4AF29D")]
        [TestCase("9BB37389-6608-4699-8592-86F269AE15B9")]
        public async Task Should_CallUserRepositoryUserExists_WhenUserIdIsValid(string userIdString)
        {
            // Arrange
            var userId = Guid.Parse(userIdString);
            MockUserRepository.Setup(s => s.UserExists(userId, It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            // Act
            await GetService().UserExists(userId, GetCancellationToken());

            // Assert
            MockUserRepository.Verify(s => s.UserExists(userId, It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
