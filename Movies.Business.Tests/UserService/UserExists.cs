using System;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Movies.Business.Tests.UserService
{
    [TestFixture]
    public class UserExists: UserServiceBase
    {

        [Test]
        public void Should_ThrowException_WhenUserIdIsInvalid()
        {
            // Arrange / Act / Assert
            Assert.That(() => GetService().UserExistsAsync(Guid.Empty), Throws.ArgumentException);
        }

        [TestCase("3763B7C5-992F-4691-8C64-88A70EC11550")]
        [TestCase("B216DDE8-EC63-41C8-B5E1-91430F4AF29D")]
        [TestCase("9BB37389-6608-4699-8592-86F269AE15B9")]
        public void Should_CallUserRepositoryUserExists_WhenUserIdIsValid(string userIdString)
        {
            // Arrange
            var userId = Guid.Parse(userIdString);
            MockUserRepository.Setup(s => s.UserExistsAsync(userId)).Returns(Task.FromResult(true));

            // Act
            var result = GetService().UserExistsAsync(userId);

            // Assert
            MockUserRepository.Verify(s => s.UserExistsAsync(userId), Times.Once);
        }

    }
}
