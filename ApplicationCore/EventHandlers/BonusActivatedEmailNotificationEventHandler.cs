using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TheRoom.PromoCodes.ApplicationCore.Events;
using TheRoom.PromoCodes.ApplicationCore.Interfaces;

namespace TheRoom.PromoCodes.ApplicationCore.EventHandlers
{
    public class BonusActivatedEmailNotificationEventHandler : INotificationHandler<BonusActivatedEvent>
    {
        private readonly IEmailSender _emailSender;

        public BonusActivatedEmailNotificationEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task Handle(BonusActivatedEvent notification, CancellationToken cancellationToken)
        {
            // This will notify the user via email of the bonus they would have activated.
            // Ideally the user email will come from the user store so this handler should be able to query for the user email based on the user id.
            // An email template should be used instead of hardcodig the message.
            return _emailSender.SendEmailAsync("user-email", $"Bonus Activated - {notification.ServiceId}", "Bonus for service was activated");
        }
    }
}
