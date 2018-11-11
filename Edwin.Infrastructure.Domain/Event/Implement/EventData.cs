﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.Event.Implement
{
    public class EventData : IEventData
    {
        public DateTime EventTime { get; set; }
        public object EventSource { get; set; }
    }
}
