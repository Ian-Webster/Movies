using NUnit.Framework;
using System.Linq;

namespace Movies.Repository.Tests.User
{
    [TestFixture]
    public class UserExists: UserRepositoryBase
    {

        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnTrueWhenUserExists(bool userExists)
        {
            //arrange
            var userId = 0;

            using (var context = GetContext())
            {
                if (userExists)
                {
                    userId = context.UserDbSet.First().Id;
                }
                else
                {
                    userId = context.UserDbSet.Max(m => m.Id) + 1;
                }
            }

            //act
            var asyncResult = GetRepository().UserExistsAsync(userId);

            //assert
            var result = asyncResult.Result;
            if(userExists)
            {
                Assert.IsTrue(result);
            }
            else
            {
                Assert.IsFalse(result);
            }
        }

    }
}
