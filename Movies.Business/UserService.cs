using Movies.Business.Interfaces;
using Movies.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
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

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("userId cannot be empty", nameof(userId));
            }

            return await _userRepository.UserExistsAsync(userId);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public async Task<List<User>> AllUsersAsync()
        {
            return await _userRepository.AllUsersAsync();
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            return await _userRepository.GetUserAsync(userId);
        }

        public async Task<bool> SaveUserAsync(User user)
        {
            return await _userRepository.SaveUserAsync(user);
        }
    }
}
