using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Domian
{
    public class IdentityEntity : IEntity<int>
    {
        public int Id { get; set; }
    }
}
