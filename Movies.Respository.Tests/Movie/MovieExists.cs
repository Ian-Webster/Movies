using System;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Repository.Tests.Movie
{
    [TestFixture]
    public class MovieExists : MovieRepositoryBase
    {
        [TestCase(true)]
        [TestCase(false)]
        public async Task Should_ReturnTrue_WhenMoviesExists(bool moviesExists)
        {
            //arrange
            Guid movieId = Guid.Empty;
            using (var context = GetContext())
            {
                if (moviesExists)
                {
                    movieId = context.MovieDbSet.First().Id;
                }
                else
                {
                    movieId = Guid.NewGuid();
                }
            }

            //act
            var result = await GetRepository().MovieExistsAsync(movieId);

            //assert
            if (moviesExists)
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
