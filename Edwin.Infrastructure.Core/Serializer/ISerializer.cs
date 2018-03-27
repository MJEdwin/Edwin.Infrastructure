using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Core.Serializer
{
    public interface ISerializer<TSourse, TDestination>
    {
        TDestination Serialize(TSourse sourse);

        TSourse Deserialize(TDestination destination);
    }
}
