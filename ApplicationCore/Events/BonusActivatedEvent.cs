using System;
using TheRoom.PromoCodes.ApplicationCore.SharedKernel;

namespace TheRoom.PromoCodes.ApplicationCore.Events
{
    public class BonusActivatedEvent : BaseDomainEvent
    {
        public Guid ServiceId { get; private set; }

        public string UserId { get; private set; }

        public BonusActivatedEvent(Guid serviceId, string userId)
        {
            Guard.AgainstNullOrEmpty(userId, nameof(userId));
            Guard.AgainstNull(serviceId, nameof(serviceId));

            ServiceId = serviceId;
            UserId = userId;
        }
    }
}
