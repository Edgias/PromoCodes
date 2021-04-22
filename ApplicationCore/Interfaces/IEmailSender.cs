using System.Threading.Tasks;

namespace TheRoom.PromoCodes.ApplicationCore.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
