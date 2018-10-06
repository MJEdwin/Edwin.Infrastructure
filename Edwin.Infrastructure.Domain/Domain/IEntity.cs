using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.Domain
{
    public interface IEntity
    {

    }

    public interface IEntity<TPrimaryKey> : IEntity
    {
        TPrimaryKey Id { get; set; }
    }
}
