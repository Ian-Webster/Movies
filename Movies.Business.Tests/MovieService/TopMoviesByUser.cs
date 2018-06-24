using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Business.Tests.MovieService
{
    [TestFixture]
    public class TopMoviesByUser : MovieServiceBase
    {
        [Test]
        public void WhenCalling_TopMoviesByUser_WithZeroMovieCount_ExpectException()
        {
            //arrage/act/assert
            Assert.That(() => GetService().TopMoviesByUser(0, 1), Throws.ArgumentException);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void WhenCalling_TopMoviesByUser_WithInvalidUserId_ExceptException(int userId)
        {
            //arrage/act/assert
            Assert.That(() => GetService().TopMoviesByUser(1, userId), Throws.ArgumentException);
        }

        [Test]
        public void WhenCalling_TopMoviesByUser_WithUserThatDoesNotExist_ExpectException()
        {
            //arrange
            MockUserService.Setup(s => s.UserExists(It.IsAny<int>())).Returns(false);

            //act/assert
            Assert.That(() => GetService().TopMoviesByUser(1, 1), Throws.ArgumentException);
        }

        [TestCase((byte)1, 3)]
        [TestCase((byte)2, 4)]
        public void WhenCalling_TopMoviesByUser_WithValidMovieCountAndUserId_RepositoryMethodCalled(byte movieCount, int userId)
        {
            //arrange
            MockUserService.Setup(s => s.UserExists(userId)).Returns(true);
            
            //act
            var result = GetService().TopMoviesByUser(movieCount, userId);

            //assert
            MockUserService.Verify(s => s.UserExists(userId), Times.Once);
            MockMovieRepository.Verify(s => s.TopMoviesByUser(movieCount, userId), Times.Once);
        }

    }
}
