using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Domain
{
    public interface IValueObject<TValueObject> : IEquatable<TValueObject>
    {
    }
}
