using Edwin.Infrastructure.Domain.Event;
using Edwin.Infrastructure.Domain.Event.Impl;
using Edwin.Infrastructure.Domain.Repositories;
using Edwin.TestApi.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edwin.TestApi.Event
{
    public class HelloWorldEventHandler : IEventHandler<EventData>
    {
        private readonly IRepository<User, string> _respository;

        public HelloWorldEventHandler(IRepository<User, string> respository)
        {
            _respository = respository;
        }

        public Task HandlerEventAsync(EventData eventData)
        {
            _respository.Add(new User
            {
                UserName = "hello,world",
                Password = "123456"
            });
            return Task.CompletedTask;
        }
    }
}
