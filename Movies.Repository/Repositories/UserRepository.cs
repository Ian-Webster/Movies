using System;
using System.Collections.Generic;
using Movies.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using repo = Movies.Repository.Entities;
using dto =Movies.Domain.DTO;

namespace Movies.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(Context context) : base(context) { }

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            return await _context.UserDbSet.AnyAsync(u => u.Id == userId);
        }

        public async Task<List<dto.User>> AllUsersAsync()
        {
            return await _context.UserDbSet.Select(s => new dto.User
            {
                Id = s.Id,
                UserName = s.UserName
            }).ToListAsync();
        }

        public async Task<dto.User> GetUserAsync(Guid userId)
        {
            return await _context.UserDbSet
                .Where(u => u.Id == userId)
                .Select(s => new dto.User
                {
                    Id = s.Id,
                    UserName = s.UserName,
                    Password = s.Password
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveUserAsync(dto.User user)
        {
            repo.User userDao = user.Id != Guid.Empty ? await _context.UserDbSet.FirstOrDefaultAsync(u => u.Id == user.Id) : null;
            if (userDao == null)
            {
                userDao = new repo.User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password
                };
                _context.UserDbSet.Add(userDao);
            }
            else
            {
                userDao.UserName = user.UserName;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
