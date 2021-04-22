using System.Linq;
using System.Threading.Tasks;
using TheRoom.PromoCodes.ApplicationCore.Entities;
using TheRoom.PromoCodes.Infrastructure.Data;
using Xunit;

namespace TheRoom.PromoCodes.IntegrationTests.Data
{
    public class EfRepositoryAdd : EfRepositoryTestFixture
    {
        [Fact]
        public async Task AddsServiceAndSetsId()
        {
            string testServiceDescription = "testService";
            EfRepository<Service> repository = GetRepository();
            Service service = new Service(testServiceDescription);

            await repository.AddAsync(service);

            Service newService = (await repository.GetAllAsync()).FirstOrDefault();

            Assert.Equal(testServiceDescription, newService.Description);
            Assert.True(newService?.Id != default);
        }
    }
}
