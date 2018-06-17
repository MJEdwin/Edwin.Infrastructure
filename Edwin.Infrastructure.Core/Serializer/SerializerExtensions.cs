using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public static class SerializerExtensions
    {
        public static T Deserialize<T, TDestination>(this ISerializer<TDestination> serializer, TDestination destination)
            where T : class
        {
            return serializer.Deserialize(destination, typeof(T)) as T;
        }
    }
}
