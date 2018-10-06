using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edwin.TestApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Edwin.TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("updatepwd")]
        public void ChangePassword(string userId, string oldPassword, string newPassword)
        {
            _userService.ChangePassword(userId, oldPassword, newPassword);
        }
    }
}
