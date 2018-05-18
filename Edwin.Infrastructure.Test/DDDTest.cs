using Microsoft.Extensions.DependencyInjection;
using Edwin.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Edwin.Infrastructure.Domain.Repositories;
using Edwin.Infrastructure.Domain.UnitOfWork;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Test
{
    public class DDDTest
    {
        private static IServiceCollection _serviceCollection = new ServiceCollection()
            .AddEntityFrameworkPool<TestDBContext>(opt => opt.UseInMemoryDatabase("test"));

        private IServiceProvider Provider => _serviceCollection.BuildServiceProvider();

        [Fact]
        public async Task ORMTestAsync()
        {
            var repoistory = Provider.GetRequiredService<IEntityRepository<Entity.Test, Guid>>();
            var manager = Provider.GetRequiredService<IUnitOfWorkManager>();
            using(var unitofwork = manager.Begin())
            {
                for (int i = 0; i < 1000; i++)
                {
                    repoistory.Add(new Entity.Test
                    {
                        Name = "Hello" + i
                    });
                }
                unitofwork.Complete();
            }

            var entities =await repoistory.FindAll().ToListAsync();

            foreach(var item in entities)
            {
                repoistory.Remove(item);
            }

        }
    }
}
