using System;

namespace TheRoom.PromoCodes.API.Models.Requests
{
    public class BonusActivationRequest : BaseRequest
    {
        public string UserId { get; set; }

        public Guid ServiceId { get; set; }
    }
}
