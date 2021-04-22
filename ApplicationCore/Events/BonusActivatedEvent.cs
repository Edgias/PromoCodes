using System;

namespace TheRoom.PromoCodes.ApplicationCore.Events
{
    public class BonusActivatedEvent : BaseDomainEvent
    {
        public Guid ServiceId { get; private set; }

        public string UserId { get; private set; }

        public BonusActivatedEvent(Guid serviceId, string userId)
        {
            ServiceId = serviceId;
            UserId = userId;
        }
    }
}
