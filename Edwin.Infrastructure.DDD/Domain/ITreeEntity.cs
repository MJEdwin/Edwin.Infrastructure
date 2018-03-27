using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Domian
{
    public interface ITreeEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        TPrimaryKey ParentId { get; set; }
    }
}
