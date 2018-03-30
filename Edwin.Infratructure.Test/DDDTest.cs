using Microsoft.Extensions.DependencyInjection;
using Edwin.Infrastructure.ORM.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Edwin.Infrastructure.DDD.Repositories;

namespace Edwin.Infratructure.Test
{
    public class DDDTest
    {
        private static IServiceCollection _serviceCollection = new ServiceCollection()
            .AddEntityFrameworkPool<TestDBContext>(opt => opt.UseInMemoryDatabase("test"));

        private IServiceProvider Provider => _serviceCollection.BuildServiceProvider();

        [Fact]
        public void ORMTest()
        {
            var repoistory = Provider.GetRequiredService<IRepository<Entity.Test, Guid>>();
            for (int i = 0; i < 1000; i++)
            {
                repoistory.Insert(new Entity.Test
                {
                    Name = "Hello" + i
                });
            }

            var entities = repoistory.FindAll();

            foreach(var item in entities)
            {
                repoistory.Delete(item);
            }

        }
    }
}
