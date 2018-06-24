namespace Movies.Repository.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Checks if the given user exists
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UserExists(int userId);
    }
}
