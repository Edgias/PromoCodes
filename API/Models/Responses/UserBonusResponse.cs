using System;

namespace TheRoom.PromoCodes.API.Models.Responses
{
    public class UserBonusResponse : BaseResponse
    {
        public string UserId { get; set; }

        public Guid ServiceId { get; set; }

        public string ServiceName { get; set; }
    }
}
