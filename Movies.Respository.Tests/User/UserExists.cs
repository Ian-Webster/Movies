using System;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Repository.Tests.User
{
    [TestFixture]
    public class UserExists: UserRepositoryBase
    {

        [TestCase(true)]
        [TestCase(false)]
        public async Task Should_ReturnTrueWhenUserExists(bool userExists)
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
            var result = await GetRepository().UserExists(userId, GetCancellationToken());

            //assert
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
