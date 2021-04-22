using System;
using TheRoom.PromoCodes.API.Interfaces;
using TheRoom.PromoCodes.API.Models.Requests;
using TheRoom.PromoCodes.API.Models.Responses;
using TheRoom.PromoCodes.ApplicationCore.Entities;

namespace TheRoom.PromoCodes.API.Services
{
    public class UserBonusMapper : IMapper<UserBonus, BonusActivationRequest, UserBonusResponse>
    {
        public UserBonus Map(BonusActivationRequest request)
        {
            UserBonus userBonus = new UserBonus(request.UserId, request.ServiceId);

            return userBonus;
        }

        public UserBonusResponse Map(UserBonus entity)
        {
            UserBonusResponse response = new UserBonusResponse
            {
                UserId = entity.UserId,
                ServiceId = entity.ServiceId,
                ServiceName = entity.Service?.Description
            };

            return response;
        }

        public void Map(UserBonus entity, BonusActivationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
