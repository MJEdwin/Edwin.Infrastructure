using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Service.Sms
{
    public class SmsConfigure
    {
        public SmsConfigure()
        {
            SmsType = new Dictionary<string, string>();
        }

        public string Id { get; set; }

        public string Secret { get; set; }

        public Dictionary<string, string> SmsType { get; set; }

        public void AddSmsType(string name, string value)
        {
            SmsType.Add(name, value);
        }
    }
}
