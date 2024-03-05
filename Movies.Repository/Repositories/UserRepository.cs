using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Repository;
using Movies.Repository.Interfaces;
using repo = Movies.Repository.Entities;
using dto =Movies.Domain.DTO;

namespace Movies.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UnitOfWork<Context> _unitOfWork;
        private readonly IRepository<repo.User> _userRepository;

        public UserRepository(UnitOfWork<Context> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.Repository<repo.User>();
        }

        public async Task<bool> UserExists(Guid userId, CancellationToken token)
        {
            return await _userRepository.Exists(u => u.Id == userId, token);
        }

        public async Task<IEnumerable<dto.User>> AllUsers(CancellationToken token)
        {
            return await _userRepository.ListProjected(u => true, u => new dto.User
                {
                    Id = u.Id,
                    UserName = u.UserName
                }, 
                token);
        }

        public async Task<dto.User?> GetUser(Guid userId, CancellationToken token)
        {
            return await _userRepository.FirstOrDefaultProjected(u => u.Id == userId,
                u => new dto.User
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Password = u.Password
                },
                token);
        }

        public async Task<bool> SaveUser(dto.User user, CancellationToken token)
        {
            var userDao = await _userRepository.FirstOrDefault(u => u.Id == user.Id, token);
            if (userDao == null)
            {
                userDao = new repo.User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password
                };
                if (! await _userRepository.Add(userDao, token)) return false;
            }
            else
            {
                userDao.UserName = user.UserName;
            }

            //TODO: reverse this once https://github.com/Ian-Webster/DataAccess/issues/30 is fixed
            //return await _unitOfWork.Save(token);
            await _unitOfWork.Save(token);
            return true;
        }

    }
}
