using System;
using System.Collections.Generic;

namespace TheRoom.PromoCodes.ApplicationCore.SharedKernel
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }

        public List<BaseDomainEvent> Events { get; private set; } = new List<BaseDomainEvent>();

        public void AddDomainEvent(BaseDomainEvent domainEvent)
        {
            Events.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseDomainEvent domainEvent)
        {
            Events.Remove(domainEvent);
        }
    }
}
