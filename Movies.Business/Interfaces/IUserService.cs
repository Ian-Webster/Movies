using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Domain.DTO;

namespace Movies.Business.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Checks if this given user exists
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UserExistsAsync(Guid userId);

        /// <summary>
        /// Hashes the given password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string HashPassword(string password);

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        Task<List<User>> AllUsersAsync();

        /// <summary>
        /// Gets a user by the given id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> GetUserAsync(Guid userId);

        /// <summary>
        /// Saves the given user
        /// </summary>
        /// <remarks>
        /// If the user doesn't exist will create one otherwise will update the existing one
        /// </remarks>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> SaveUserAsync(User user);
    }
}
