using System.Collections.Generic;
using System.Linq;
using TheRoom.PromoCodes.ApplicationCore.Entities;
using Xunit;

namespace TheRoom.PromoCodes.UnitTests.ApplicationCore.Specifications
{
    public class ServiceFilterSpecification
    {
        [Theory]
        [InlineData(2)]
        public void MatchesExpectedNumberofItems(int expectedCount)
        {
            PromoCodes.ApplicationCore.Specifications.ServiceSpecification specification = 
                new PromoCodes.ApplicationCore.Specifications.ServiceSpecification(1, 2, "Tes");

            IQueryable<Service> result = GetTestCollection()
                .AsQueryable()
                .Where(specification.Criteria)
                .Skip(1)
                .Take(2);

            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.ToList().Count);
        }

        private List<Service> GetTestCollection()
        {
            List<Service> serviceList = new List<Service>
            {
                new Service("Test 1"),
                new Service("Test 2"),
                new Service("Test 3"),
                new Service("Test 4")
            };

            return serviceList;
        }
    }
}
