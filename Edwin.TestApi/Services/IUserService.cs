using Edwin.TestApi.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edwin.TestApi.Services
{
    public interface IUserService
    {
        [Transaction]
        void ChangePassword(string userId, string oldPassword, string newPassword);
    }
}
