using Edwin.Infrastructure.Domain.Repositories;
using Edwin.TestApi.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edwin.TestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, string> _userRepository;

        public UserService(IRepository<User, string> userRepository)
        {
            _userRepository = userRepository;
        }

        public void ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var user = _userRepository.FindOrDefaultById(userId);
            if (user == null)
                throw new Exception("user is not found");
            if (user.Password != oldPassword)
                throw new Exception("password is not same");

            user.Password = newPassword;
            _userRepository.Update(user);
        }
    }
}
