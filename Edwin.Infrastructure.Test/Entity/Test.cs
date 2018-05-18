using Edwin.Infrastructure.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Test.Entity
{
    public class Test : GuidEntity,IAggregateRoot
    {
        public string Name { get; set; }
    }
}
