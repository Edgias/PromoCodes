using MediatR;
using System;

namespace TheRoom.PromoCodes.ApplicationCore.Events
{
    public abstract class BaseDomainEvent : INotification
    {
        public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
    }
}
