using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Extensions
{
    public static class ClassExtensions
    {
        public static bool IsNull<T>(this T @object)
            where T : class
        {
            return @object == null;
        }

        public static Dictionary<string, string> ToDictionary(this object @object)
        {
            var type = @object.GetType();

            var dict = new Dictionary<string, string>();
            foreach (var prop in type.GetProperties())
            {
                dict.Add(prop.Name, prop.GetValue(@object).ToString());
            }

            return dict;
        }
    }
}
