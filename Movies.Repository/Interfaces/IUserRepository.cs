using System;
using System.Threading.Tasks;

namespace Movies.Repository.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Checks if the given user exists
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UserExistsAsync(Guid userId);
    }
}
