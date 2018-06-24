﻿using Movies.Business.Interfaces;
using Movies.Repository.Interfaces;
using System;

namespace Movies.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool UserExists(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("userId must be greater than zero", "userId");
            }

            return _userRepository.UserExists(userId);
        }
    }
}
