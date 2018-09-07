using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

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
            Assert.That(() => GetService().UserExistsAsync(userId), Throws.ArgumentException);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void WhenCalling_UsersExists_WithValidUserId_RepositoryMethodCalled(int userId)
        {
            //arrange
            MockUserRepository.Setup(s => s.UserExistsAsync(userId)).Returns(Task.FromResult(true));

            //act
            var result = GetService().UserExistsAsync(userId);

            //assert
            MockUserRepository.Verify(s => s.UserExistsAsync(userId), Times.Once);
        }

    }
}
