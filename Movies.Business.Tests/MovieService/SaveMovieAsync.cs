using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using dto = Movies.Domain.DTO;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class SaveMovieAsync : MovieServiceBase
    {
        [Test]
        public void Should_CallRepositoryMethod()
        {
            //arrange
            var newMovie = new dto.Movie();

            //act
            var result = GetService().SaveMovieAsync(newMovie);

            //assert
            MockMovieRepository.Verify(s => s.SaveMovieAsync(newMovie), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnTrueWhen_SaveSucceeds(bool saveSuccess)
        {
            //arrange
            MockMovieRepository.Setup(s => s.SaveMovieAsync(It.IsAny<dto.Movie>())).Returns(Task.FromResult(saveSuccess));

            //act
            var asyncResult = GetService().SaveMovieAsync(new dto.Movie());

            //assert
            var result = asyncResult.Result;
            if (saveSuccess)
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
