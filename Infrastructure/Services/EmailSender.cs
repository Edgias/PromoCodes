using System.Net.Mail;
using System.Threading.Tasks;
using TheRoom.PromoCodes.ApplicationCore.Interfaces;

namespace TheRoom.PromoCodes.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            SmtpClient emailClient = new SmtpClient("localhost");

            MailMessage message = new MailMessage
            {
                Subject = subject,
                Body = body
            };

            message.To.Add(new MailAddress(to));

            await emailClient.SendMailAsync(message);
        }
    }
}
