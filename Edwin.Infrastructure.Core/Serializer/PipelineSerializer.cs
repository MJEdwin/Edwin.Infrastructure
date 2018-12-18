using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public class PipelineSerializer<TSerializeFrom, TMiddleware, TSerializeTo> : ISerializer<TSerializeFrom, TSerializeTo>
    {
        private readonly ISerializer<TSerializeFrom, TMiddleware> _perivous;
        private readonly ISerializer<TMiddleware, TSerializeTo> _next;

        public PipelineSerializer(ISerializer<TSerializeFrom, TMiddleware> perivous, ISerializer<TMiddleware, TSerializeTo> next)
        {
            _perivous = perivous;
            _next = next;
        }

        public TSerializeTo Serialize(TSerializeFrom sourse)
            => _next.Serialize(_perivous.Serialize(sourse));

        public TSerializeFrom Deserialize(TSerializeTo destination)
            => _perivous.Deserialize(_next.Deserialize(destination));

        public PipelineSerializer<TSerializeFrom, TSerializeTo, TNext> Append<TNext>(ISerializer<TSerializeTo, TNext> serializer)
            => new PipelineSerializer<TSerializeFrom, TSerializeTo, TNext>(this, serializer);
    }
}
