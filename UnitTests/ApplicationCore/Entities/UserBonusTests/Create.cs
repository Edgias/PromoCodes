using System;
using System.Linq;
using TheRoom.PromoCodes.ApplicationCore.Entities;
using TheRoom.PromoCodes.ApplicationCore.Events;
using Xunit;

namespace TheRoom.PromoCodes.UnitTests.ApplicationCore.Entities.UserBonusTests
{
    public class Create
    {
        private UserBonus _userBonus;
        private string _validUserId = "AF63D7C3-E803-44F9-30D6-08D87656AB86";
        private Guid _validServiceId = new Guid("4F916307-57CF-46C5-E57D-08D8894AFEB2");

        public Create()
        {
            _userBonus = new UserBonus(_validUserId, _validServiceId);
        }

        [Fact]
        public void ThrowsArgumentExceptionGivenEmptyUserId()
        {
            string newValue = "";
            Assert.Throws<ArgumentException>(() => new UserBonus(newValue, _validServiceId));
        }

        [Fact]
        public void RaisesBonusActivatedEventEvent()
        {
            Assert.Single(_userBonus.Events);
            Assert.IsType<BonusActivatedEvent>(_userBonus.Events.First());
        }
    }
}
