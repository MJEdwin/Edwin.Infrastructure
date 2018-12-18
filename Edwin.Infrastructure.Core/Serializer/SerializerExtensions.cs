using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public static class SerializerExtensions
    {
        public static ISerializer<TSerializeFrom, TSerializeTo> Append<TSerializeFrom, TMiddleware, TSerializeTo>(this ISerializer<TSerializeFrom, TMiddleware> serializer, ISerializer<TMiddleware, TSerializeTo> next) 
            => new PipelineSerializer<TSerializeFrom, TMiddleware, TSerializeTo>(serializer, next);

        public static ISerializer<TTo, TFrom> Reverse<TFrom, TTo>(this ISerializer<TFrom, TTo> serializer)
            => new ReverseSerializer<TTo, TFrom>(serializer);
    }
}
