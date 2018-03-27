using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edwin.Infrastructure.AutoMapper
{
    public static class AutoMapperExtensions
    {
        //静态初始化
        private static IMapper _mapper = AutoMapperConfiguration.Instance.GetMapper();

        public static IEnumerable<TDes> MapTo<TSourse, TDes>(this IEnumerable<TSourse> Sourse)
            => Sourse.Select(sourse => sourse.MapTo<TSourse, TDes>());

        public static IQueryable<TDes> MapTo<TSourse, TDes>(this IQueryable<TSourse> Sourse)
            => Sourse.Select(sourse => sourse.MapTo<TSourse, TDes>());

        public static TDes MapTo<TSourse, TDes>(this TSourse Sourse)
            => _mapper.Map<TSourse, TDes>(Sourse);

        public static TDes MapTo<TSourse, TDes>(this TSourse Sourse, TDes Des)
            => _mapper.Map(Sourse, Des);
    }
}
