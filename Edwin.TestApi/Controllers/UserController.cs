using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edwin.Infrastructure.Domain.Event;
using Edwin.Infrastructure.Domain.Event.Impl;
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
        private readonly IEventBus _eventBus;
        public UserController(IUserService userService, IEventBus eventBus)
        {
            _userService = userService;
            _eventBus = eventBus;
        }

        [HttpPut("updatepwd")]
        public void ChangePassword(string userId, string oldPassword, string newPassword)
        {
            _userService.ChangePassword(userId, oldPassword, newPassword);
        }
        [HttpPost]
        public void Post()
        {
            _eventBus.Publish(new EventData
            {
                EventSource = this,
                EventTime = DateTime.Now
            });
        }
    }
}
