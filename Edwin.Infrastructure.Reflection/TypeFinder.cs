using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Edwin.Infrastructure.Reflection
{
    public class TypeFinder
    {
        public static IEnumerable<TypeInfo> GetTypes()
        {
            var assemblies = AssemblyFinder.GetCustomerAssemblies().Select(a => Assembly.Load(a));
            List<TypeInfo> types = new List<TypeInfo>();
            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly.DefinedTypes);
            }
            return types.Distinct();
        }

        public static IEnumerable<TypeInfo> GetTypes(Func<TypeInfo, bool> where)
        {
            return GetTypes().Where(where);
        }
    }
}
