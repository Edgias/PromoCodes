using System.Collections.Generic;
using System.Linq;
using TheRoom.PromoCodes.ApplicationCore.Entities;
using Xunit;

namespace TheRoom.PromoCodes.UnitTests.ApplicationCore.Specifications
{
    public class ServiceFilterSpecification
    {
        [Fact]
        public void Returns2ServicesSkippingTheFirstOne()
        {
            PromoCodes.ApplicationCore.Specifications.ServiceSpecification specification = 
                new PromoCodes.ApplicationCore.Specifications.ServiceSpecification(1, 2, "Tes");

            IQueryable<Service> result = GetTestCollection()
                .AsQueryable()
                .Where(specification.Criteria);

            Assert.NotNull(result);
            Assert.Equal(2, result.ToList().Count);
        }

        private List<Service> GetTestCollection()
        {
            var catalogItemList = new List<Service>();

            catalogItemList.Add(new Service("Test 1"));
            catalogItemList.Add(new Service("Test 2"));
            catalogItemList.Add(new Service("Test 3"));
            catalogItemList.Add(new Service("Test 4"));

            return catalogItemList;
        }
    }
}
