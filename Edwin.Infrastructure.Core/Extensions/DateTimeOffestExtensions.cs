using Edwin.Infrastructure.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Extensions
{
    public static class DateTimeOffestExtensions
    {
        public static TimeStamp ToTimeStamp(this DateTimeOffset dateTimeOffset)
            => dateTimeOffset;
    }
}
