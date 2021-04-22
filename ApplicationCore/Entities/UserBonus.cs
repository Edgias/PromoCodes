using System;
using TheRoom.PromoCodes.ApplicationCore.Events;
using TheRoom.PromoCodes.ApplicationCore.SharedKernel;

namespace TheRoom.PromoCodes.ApplicationCore.Entities
{
    public class UserBonus : BaseEntity
    {
        public string UserId { get; private set; }

        public Guid ServiceId { get; private set; }

        public Service Service { get; private set; }

        public UserBonus(string userId, Guid serviceId)
        {
            Guard.AgainstNullOrEmpty(userId, nameof(userId));
            Guard.AgainstNull(serviceId, nameof(serviceId));

            UserId = userId;
            ServiceId = serviceId;
            AddDomainEvent(new BonusActivatedEvent(serviceId, userId));
        }
    }
}
