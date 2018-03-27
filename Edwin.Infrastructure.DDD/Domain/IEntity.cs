using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Domian
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
