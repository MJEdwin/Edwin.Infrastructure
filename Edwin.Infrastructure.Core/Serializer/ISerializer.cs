using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public interface ISerializer<TSerializeFrom, TSerializeTo>
    {
        TSerializeTo Serialize(TSerializeFrom sourse);

        TSerializeFrom Deserialize(TSerializeTo destination);
    }
}
