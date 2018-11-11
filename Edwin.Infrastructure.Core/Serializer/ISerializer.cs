using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public interface ISerializer<TDestination>
    {
        TDestination Serialize(object sourse);

        object Deserialize(TDestination destination,Type sourseType);
    }
}
