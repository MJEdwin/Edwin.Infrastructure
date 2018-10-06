using Edwin.Infrastructure.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static TimeStamp ToTimeStamp(this DateTime dateTime)
            => new TimeStamp(dateTime);
    }
}
