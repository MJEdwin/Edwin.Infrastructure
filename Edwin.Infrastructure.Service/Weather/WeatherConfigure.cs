using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace BingoCore.Infrastructure.Service.Weather
{
    public class WeatherConfigure
    {
        public string Key { get; set; }
        public string Location { get; set; }
        public string Language { get; set; }
        public string Unit { get; set; }

        public string ToUrlParameter()
        {
            List<string> parm = new List<string>();
            foreach (var prop in GetType().GetRuntimeProperties())
            {
                parm.Add(prop.Name.ToLower() + "=" + prop.GetValue(this));
            }
            return string.Join("&", parm);
        }
    }
}
