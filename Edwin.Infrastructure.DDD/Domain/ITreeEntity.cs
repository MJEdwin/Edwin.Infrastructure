using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Domain
{
    public interface ITreeEntity<TPrimaryKey> : IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        TPrimaryKey? ParentId { get; set; }
    }
}
