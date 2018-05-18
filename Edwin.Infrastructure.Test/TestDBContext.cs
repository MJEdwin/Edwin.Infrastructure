using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Test
{
    public class TestDBContext : DbContext
    {
        public TestDBContext(DbContextOptions<TestDBContext> options):base(options)
        {

        }

        public DbSet<Entity.Test> Tests { get; set; }
    }
}
