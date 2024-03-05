using Movies.Business.Interfaces;
using Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Movies.Domain.DTO;

namespace Movies.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> UserExists(Guid userId, CancellationToken token)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("userId cannot be empty", nameof(userId));
            }

            return await _userRepository.UserExists(userId, token);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public async Task<IEnumerable<User>> AllUsers(CancellationToken token)
        {
            return await _userRepository.AllUsers(token);
        }

        public async Task<User> GetUser(Guid userId, CancellationToken token)
        {
            return await _userRepository.GetUser(userId, token);
        }

        public async Task<bool> SaveUser(User user, CancellationToken token)
        {
            return await _userRepository.SaveUser(user, token);
        }
    }
}
