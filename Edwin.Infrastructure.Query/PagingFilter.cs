using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Edwin.Infrastructure.Query
{
    public class PagingFilter<TEntity> : IFilter<TEntity>
    {
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageNumber { get; set; }

        public IQueryable<TEntity> Query(IQueryable<TEntity> queryable)
        {
            return queryable.Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }

        public static explicit operator PagingFilter<TEntity>(JObject jObject)
            => new PagingFilter<TEntity>
            {
                PageSize = jObject["pageSize"].Value<int>(),
                PageNumber = jObject["pageNumber"].Value<int>()
            };
    }
}
