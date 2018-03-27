using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Edwin.Infrastructure.Reflection
{
    public class AssemblyFinder
    {
        public static string[] NotNeedAssemblyNames = new string[] { "System", "Microsoft", "Newtonsoft" };

        public static Assembly GetDomainAssembly()
            => Assembly.GetEntryAssembly();

        public static IEnumerable<AssemblyName> GetAssemblies()
            => GetDomainAssembly().GetReferencedAssemblies().Distinct().Append(GetDomainAssembly().GetName());

        public static IEnumerable<AssemblyName> GetCustomerAssemblies() 
            => GetAssemblies().Where(an => !NotNeedAssemblyNames.Any(nan => an.FullName.Contains(nan)));
    }
}
