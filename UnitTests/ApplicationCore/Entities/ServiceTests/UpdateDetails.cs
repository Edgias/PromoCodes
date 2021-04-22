using System;
using TheRoom.PromoCodes.ApplicationCore.Entities;
using Xunit;

namespace TheRoom.PromoCodes.UnitTests.ApplicationCore.Entities.ServiceTests
{
    public class UpdateDetails
    {
        private Service _testService;
        private string _validDescription = "Test Service";

        public UpdateDetails()
        {
            _testService = new Service(_validDescription);
        }

        [Fact]
        public void ThrowsArgumentExceptionGivenEmptyDescription()
        {
            string newValue = "";
            Assert.Throws<ArgumentException>(() => _testService.UpdateDetails(newValue));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenNullDescription()
        {
            Assert.Throws<ArgumentNullException>(() => _testService.UpdateDetails(null));
        }
    }
}
