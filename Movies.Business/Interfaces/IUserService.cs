using System.Threading.Tasks;

namespace Movies.Business.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Checks if this given user exists
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UserExistsAsync(int userId);
    }
}
