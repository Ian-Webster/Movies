using Movies.Repository.Interfaces;
using System.Linq;

namespace Movies.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(Context context) : base(context) { }

        public bool UserExists(int userId)
        {
            return _context.UserDbSet.Any(u => u.Id == userId);
        }
    }
}
