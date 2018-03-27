﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Domian
{
    public class GuidEntity : IEntity<Guid>
    {
        public GuidEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
