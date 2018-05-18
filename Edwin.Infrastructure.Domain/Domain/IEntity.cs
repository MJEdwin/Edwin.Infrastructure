using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Edwin.Infrastructure.Domain.Domain
{
    public interface IEntity
    {

    }

    public interface IEntity<TPrimaryKey> : IEntity
    {
        [Key]
        TPrimaryKey Id { get; set; }
    }
}
