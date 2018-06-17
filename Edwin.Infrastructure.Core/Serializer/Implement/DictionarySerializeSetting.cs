using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Serializer
{
    public class DictionarySerializeSetting
    {
        public DictionarySerializeSetting()
        {

        }

        public NameConvert NameConvert { get; set; }

        public string ConvertName(string name)
        {
            switch (NameConvert)
            {
                case NameConvert.Default:
                default:
                    return name;
                case NameConvert.ToLower:
                    return name.ToLower();
                case NameConvert.ToUpper:
                    return name.ToUpper();
            }
        }
    }

    public enum NameConvert
    {
        Default = 1,
        ToLower = 2,
        ToUpper = 4,
    }
}
