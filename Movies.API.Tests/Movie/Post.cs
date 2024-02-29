using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using dto = Movies.Domain.DTO;

namespace Movies.API.Tests.Movie
{
    [TestFixture]
    public class Post: MovieBase
    {

        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnOKStatus_WhenMovieIsSaved(bool isSaved)
        {
            //arrange
            MockMovieService.Setup(s => s.SaveMovieAsync(It.IsAny<dto.Movie>())).Returns(Task.FromResult(isSaved));

            //act
            var asyncResult = GetController().Post(new dto.Movie());

            //assert
            var result = asyncResult.Result;

            if (isSaved)
            {
                Assert.That(result, Is.InstanceOf<OkResult>());
            }
            else
            {
                Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            }
        }

    }
}
