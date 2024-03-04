using System;
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
            Guid userId = Guid.Empty;

            using (var context = GetContext())
            {
                if (userExists)
                {
                    userId = context.UserDbSet.First().Id;
                }
                else
                {
                    userId = Guid.NewGuid();
                }
            }

            //act
            var asyncResult = GetRepository().UserExistsAsync(userId);

            //assert
            var result = asyncResult.Result;
            if(userExists)
            {
                Assert.That(result, Is.True);
            }
            else
            {
                Assert.That(result, Is.False);
            }
        }

    }
}
