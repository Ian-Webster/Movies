using Movies.Domain.DTO;
using Movies.Domain.Enums.Validation;
using NUnit.Framework;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class ValidateSearchCriteria : MovieServiceBase
    {

        [Test]
        public void WhenCalling_ValidateSearchCriteria_WithNullCriteria_ExpectedResultReturned()
        {
            //arrange/act
            var result = GetService().ValidateSearchCriteria(null);

            //assert
            Assert.AreEqual(result, MovieSearchValidationResults.NoCriteria);
        }

        [TestCase("test", 0, false, MovieSearchValidationResults.OK)]
        [TestCase("", 1999, false, MovieSearchValidationResults.OK)]
        [TestCase("", 0, true, MovieSearchValidationResults.OK)]
        [TestCase("", 0, false, MovieSearchValidationResults.InvalidCriteria)]
        public void WhenCalling_ValidateSearchCriteria_WithNonNullCriteria_ExpectedResultReturned(string movieTitle, short yearOfRelease, bool hasGenres, MovieSearchValidationResults expectedResult)
        {
            //arrange
            var criteria = new MovieSearchCriteria
            {
                Title = movieTitle,
                YearOfRelease = yearOfRelease,
                Genres = hasGenres? GetGenreList() : null
            };

            //act
            var result = GetService().ValidateSearchCriteria(criteria);

            //assert
            Assert.AreEqual(result, expectedResult);
        }

    }
}
