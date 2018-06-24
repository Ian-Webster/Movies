using Moq;
using NUnit.Framework;

namespace Movies.Business.Tests.UserService
{
    [TestFixture]
    public class UserExists: UserServiceBase
    {

        [TestCase(-1)]
        [TestCase(0)]
        public void WhenCalling_UsersExists_WithInvalidUserId_ExpectException(int userId)
        {
            //arrange/act/assert
            Assert.That(() => GetService().UserExists(userId), Throws.ArgumentException);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void WhenCalling_UsersExists_WithValidUserId_RepositoryMethodCalled(int userId)
        {
            //arrange
            MockUserRepository.Setup(s => s.UserExists(userId)).Returns(true);

            //act
            var result = GetService().UserExists(userId);

            //assert
            MockUserRepository.Verify(s => s.UserExists(userId), Times.Once);
        }

    }
}
