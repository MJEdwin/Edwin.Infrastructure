using Edwin.Infrastructure.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edwin.TestApi.Domain.User
{
    public class User : IStringEntity,IAggregateRoot
    {
        public string Id { get; set; }
        
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
