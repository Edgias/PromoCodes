namespace TheRoom.PromoCodes.API.Models.Requests
{
    public class AuthenticateRequest : BaseRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
