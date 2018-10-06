using Edwin.Infrastructure.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edwin.TestApi.Domain.Post
{
    /// <summary>
    /// 博客
    /// </summary>
    public class Post : IStringEntity
    {

        public string Id { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }
    }
}
