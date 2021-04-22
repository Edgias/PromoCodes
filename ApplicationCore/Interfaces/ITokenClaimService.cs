using System.Threading.Tasks;

namespace TheRoom.PromoCodes.ApplicationCore.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
    }
}
