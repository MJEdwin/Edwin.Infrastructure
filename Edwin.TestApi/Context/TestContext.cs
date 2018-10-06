using Edwin.TestApi.Domain.Post;
using Edwin.TestApi.Domain.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edwin.TestApi.Context
{
    public class TestContext : DbContext
    {
        public virtual DbSet<Blog> Blogs { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(opt =>
            {
                opt.Property(u => u.UserName)
                .HasMaxLength(50);
            });
        }
    }
}
