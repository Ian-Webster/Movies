using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Movies.Domain.DTO;

namespace Movies.Repository.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Checks if the given user exists
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> UserExists(Guid userId, CancellationToken token);

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> AllUsers(CancellationToken token);

        /// <summary>
        /// Gets a user by the given id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<User?> GetUser(Guid userId, CancellationToken token);

        /// <summary>
        /// Saves the given user
        /// </summary>
        /// <remarks>
        /// If the user doesn't exist will create one otherwise will update the existing one
        /// </remarks>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> SaveUser(User user, CancellationToken token);
    }
}
