using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edwin.Infrastructure.Domain.Domain
{
    public abstract class ValueObject : IValueObject<ValueObject>
    {
        public abstract IEnumerable<object> GetAtomicValues();
        public bool Equals(ValueObject other)
        {
            var srcPropCollection = GetAtomicValues();
            var destPropCollection = other.GetAtomicValues();
            for (int i = 0; i < srcPropCollection.Count(); i++)
            {
                if (srcPropCollection.ElementAt(i) != destPropCollection.ElementAt(i))
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;
            return Equals(obj as ValueObject);
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject value1, ValueObject value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(ValueObject value1, ValueObject value2)
        {
            return !value1.Equals(value2);
        }
    }
}
