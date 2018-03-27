using AutoMapper;
using Edwin.Infrastructure.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Edwin.Infrastructure.AutoMapper
{
    public class AutoMapperConfiguration
    {
        private MapperConfiguration _instance;

        public AutoMapperConfiguration(IEnumerable<Profile> profiles)
        {
            _instance = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
        }

        public AutoMapperConfiguration(string[] assemblyNames)
        {
            _instance = new MapperConfiguration(cfg =>
            {
                //允许空集映射
                cfg.AddProfiles(assemblyNames);
            });
        }

        public IMapper GetMapper()
            => _instance.CreateMapper();

        public static AutoMapperConfiguration Instance { get; } = new AutoMapperConfiguration(AssemblyFinder.GetCustomerAssemblies().Select(a => a.FullName).ToArray());
    }
}
