using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TheRoom.PromoCodes.ApplicationCore.Entities;
using TheRoom.PromoCodes.Infrastructure.Data;

namespace TheRoom.PromoCodes.IntegrationTests.Data
{
    public class EfRepositoryTestFixture
    {
        protected PromoCodesDbContext _dbContext;

        protected static DbContextOptions<PromoCodesDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<PromoCodesDbContext>();
            builder.UseInMemoryDatabase("promocodes")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected EfRepository<Service> GetRepository()
        {
            var options = CreateNewContextOptions();
            var mockMediator = new Mock<IMediator>();

            _dbContext = new PromoCodesDbContext(options, mockMediator.Object);
            return new EfRepository<Service>(_dbContext);
        }
    }
}
