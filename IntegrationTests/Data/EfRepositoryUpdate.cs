using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheRoom.PromoCodes.ApplicationCore.Entities;
using Xunit;

namespace TheRoom.PromoCodes.IntegrationTests.Data
{
    public class EfRepositoryUpdate : EfRepositoryTestFixture
    {
        [Fact]
        public async Task UpdatesServiceAfterAddingIt()
        {
            // add a service
            var repository = GetRepository();
            var initialName = Guid.NewGuid().ToString();
            var service = new Service(initialName);

            await repository.AddAsync(service);

            // detach the service so we get a different instance
            _dbContext.Entry(service).State = EntityState.Detached;

            // fetch the service and update its description
            var newService = (await repository.GetAllAsync())
                .FirstOrDefault(service => service.Description == initialName);
            Assert.NotNull(newService);
            Assert.NotSame(service, newService);
            var newName = Guid.NewGuid().ToString();
            newService.UpdateDetails(newName);

            // Update the service
            await repository.UpdateAsync(newService);

            // Fetch the updated service
            var updatedService = (await repository.GetAllAsync())
                .FirstOrDefault(service => service.Description == newName);

            Assert.NotNull(updatedService);
            Assert.NotEqual(service.Description, updatedService.Description);
            Assert.Equal(newService.Id, updatedService.Id);
        }
    }
}
