using Edwin.Infrastructure.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edwin.TestApi.Domain.Post
{
    public class Blog : IStringEntity
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
