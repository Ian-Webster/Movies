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
            var movieId = 0;
            using (var context = GetContext())
            {
                if (moviesExists)
                {
                    movieId = context.MovieDbSet.First().Id;
                }
                else
                {
                    movieId = context.MovieDbSet.Max(m => m.Id) + 1;
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
