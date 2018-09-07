using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class TopMoviesByUser : MovieServiceBase
    {
        [Test]
        public void WhenCalling_TopMoviesByUser_WithZeroMovieCount_ExpectException()
        {
            //arrage/act/assert
            Assert.That(() => GetService().TopMoviesByUserAsync(0, 1), Throws.ArgumentException);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void WhenCalling_TopMoviesByUser_WithInvalidUserId_ExceptException(int userId)
        {
            //arrage/act/assert
            Assert.That(() => GetService().TopMoviesByUserAsync(1, userId), Throws.ArgumentException);
        }

        [Test]
        public void WhenCalling_TopMoviesByUser_WithUserThatDoesNotExist_ExpectException()
        {
            //arrange
            MockUserService.Setup(s => s.UserExistsAsync(It.IsAny<int>())).Returns(Task.FromResult(false));

            //act/assert
            Assert.That(() => GetService().TopMoviesByUserAsync(1, 1), Throws.ArgumentException);
        }

        [TestCase((byte)1, 3)]
        [TestCase((byte)2, 4)]
        public void WhenCalling_TopMoviesByUser_WithValidMovieCountAndUserId_RepositoryMethodCalled(byte movieCount, int userId)
        {
            //arrange
            MockUserService.Setup(s => s.UserExistsAsync(userId)).Returns(Task.FromResult(true));
            
            //act
            var result = GetService().TopMoviesByUserAsync(movieCount, userId);

            //assert
            MockUserService.Verify(s => s.UserExistsAsync(userId), Times.Once);
            MockMovieRepository.Verify(s => s.TopMoviesByUserAsync(movieCount, userId), Times.Once);
        }

    }
}
