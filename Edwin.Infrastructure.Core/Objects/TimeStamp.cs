using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Objects
{
    public struct TimeStamp : IComparable, IComparable<TimeStamp>, IEquatable<TimeStamp>, IFormattable
    {
        public TimeSpan TimeSpan { get; private set; }
        public long Ticks => TimeSpan.Ticks;
        public double TotalMilliseconds => TimeSpan.TotalMilliseconds;
        public double TotalSeconds => TimeSpan.TotalSeconds;
        public DateTimeOffset DateTimeOffset => ReferenceTime + TimeSpan;
        public DateTime ToUTCDateTime() => DateTimeOffset.UtcDateTime;
        public DateTime ToLocalDateTime() => DateTimeOffset.LocalDateTime;

        public static readonly TimeStamp Zero = ReferenceTime;
        public static TimeStamp Now => DateTimeOffset.UtcNow - ReferenceTime;
        public static readonly DateTimeOffset ReferenceTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
        public TimeStamp(long ticks)
        {
            TimeSpan = new TimeSpan(ticks);
        }

        public TimeStamp(double milliseconds)
        {
            TimeSpan = new TimeSpan((long)milliseconds * TimeSpan.TicksPerMillisecond);
        }

        public TimeStamp(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;
        }

        public TimeStamp(DateTime dateTime)
        {
            TimeSpan = dateTime.ToUniversalTime() - ReferenceTime;
        }

        public TimeStamp(DateTimeOffset dateTime)
        {
            TimeSpan = dateTime.UtcDateTime - ReferenceTime;
        }
        public int CompareTo(object obj)
        {
            return CompareTo((TimeStamp)obj);
        }

        public int CompareTo(TimeStamp other)
        {
            return TimeSpan.CompareTo(other.TimeSpan);
        }

        public bool Equals(TimeStamp other)
        {
            return TimeSpan.Equals(other.TimeSpan);
        }

        public override int GetHashCode()
        {
            return TimeSpan.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals((TimeStamp)obj);
        }

        public string ToString(string format)
        {
            return TimeSpan.ToString(format);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return TimeSpan.ToString(format, formatProvider);
        }
        public override string ToString()
        {
            return TimeSpan.ToString();
        }

        public static implicit operator TimeSpan(TimeStamp timeStamp) => timeStamp.TimeSpan;
        public static implicit operator TimeStamp(TimeSpan timeSpan) => new TimeStamp(timeSpan);
        public static implicit operator TimeStamp(DateTime dateTime) => new TimeStamp(dateTime);
        public static implicit operator TimeStamp(DateTimeOffset dateTimeOffset) => new TimeStamp(dateTimeOffset);
        public static implicit operator DateTimeOffset(TimeStamp timeStamp) => timeStamp.DateTimeOffset;

        public static bool operator ==(TimeStamp t1, TimeStamp t2) => t1.Equals(t2);
        public static bool operator !=(TimeStamp t1, TimeStamp t2) => t1.Equals(t2);
        public static bool operator >(TimeStamp t1, TimeStamp t2) => t1.CompareTo(t2) > 0;
        public static bool operator <(TimeStamp t1, TimeStamp t2) => t1.CompareTo(t2) < 0;
        public static bool operator >=(TimeStamp t1, TimeStamp t2) => t1.CompareTo(t2) >= 0;
        public static bool operator <=(TimeStamp t1, TimeStamp t2) => t1.CompareTo(t2) <= 0;
    }
}
