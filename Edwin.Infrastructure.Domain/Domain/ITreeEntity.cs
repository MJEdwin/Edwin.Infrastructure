using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.Domain
{
    public interface ITreeEntity<TPrimaryKey> : IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        TPrimaryKey? ParentId { get; set; }
    }
}
