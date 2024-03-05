using Moq;
using Movies.Business.Interfaces;
using Movies.Business.Tests.Shared;
using Movies.Repository.Interfaces;

namespace Movies.Business.Tests.UserService
{
    public class UserServiceBase: BusinessServiceBase
    {
        protected readonly Mock<IUserRepository> MockUserRepository;

        public UserServiceBase()
        {
            MockUserRepository = new Mock<IUserRepository>();
        }

        public IUserService GetService()
        {
            return new Business.UserService(MockUserRepository.Object);
        }

    }
}
