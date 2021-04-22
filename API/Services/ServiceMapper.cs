using TheRoom.PromoCodes.API.Interfaces;
using TheRoom.PromoCodes.API.Models.Requests;
using TheRoom.PromoCodes.API.Models.Responses;
using TheRoom.PromoCodes.ApplicationCore.Entities;

namespace TheRoom.PromoCodes.API.Services
{
    public class ServiceMapper : IMapper<Service, ServiceRequest, ServiceResponse>
    {
        public Service Map(ServiceRequest request)
        {
            Service entity = new Service(request.Description);

            return entity;
        }

        public ServiceResponse Map(Service entity)
        {
            ServiceResponse response = new ServiceResponse
            {
                Id = entity.Id,
                Description = entity.Description
            };

            return response;
        }

        public void Map(Service entity, ServiceRequest request)
        {
            entity.UpdateDetails(request.Description);
        }
    }
}
