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

        /// <summary>
        /// 获取映射对象
        /// </summary>
        /// <returns></returns>
        public IMapper GetMapper()
            => _instance.CreateMapper();

        /// <summary>
        /// AutoMapper静态初始化
        /// </summary>
        public static AutoMapperConfiguration Instance { get; } = new AutoMapperConfiguration(AssemblyFinder.GetCustomerAssemblies().Select(a => a.FullName).ToArray());
    }
}
