using NUnit.Framework;
using System.Linq;

namespace Movies.Repository.Tests.Movie
{
    [TestFixture]
    public class MovieExists : MovieRepositoryBase
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnTrueWhenMoviesExists(bool moviesExists)
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
            var result = GetRepository().MovieExistsAsync(movieId);

            //assert
            if (moviesExists)
            {
                Assert.IsTrue(result.Result);
            }
            else
            {
                Assert.IsFalse(result.Result);
            }
        }

    }
}
