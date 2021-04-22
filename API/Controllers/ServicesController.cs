using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRoom.PromoCodes.API.Interfaces;
using TheRoom.PromoCodes.API.Models.Requests;
using TheRoom.PromoCodes.API.Models.Responses;
using TheRoom.PromoCodes.ApplicationCore.Entities;
using TheRoom.PromoCodes.ApplicationCore.Interfaces;
using TheRoom.PromoCodes.ApplicationCore.Specifications;

namespace TheRoom.PromoCodes.API.Controllers
{
    [Authorize]
    [Route("v1.0/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IAppLogger<ServicesController> _logger;
        private readonly IAsyncRepository<Service> _repository;
        private readonly IAsyncRepository<UserBonus> _userBonusRepository;
        private readonly IMapper<Service, ServiceRequest, ServiceResponse> _mapper;
        private readonly IMapper<UserBonus, BonusActivationRequest, UserBonusResponse> _userBonusMapper;

        public ServicesController(IAppLogger<ServicesController> logger,
            IAsyncRepository<Service> repository,
            IAsyncRepository<UserBonus> userBonusRepository,
            IMapper<Service, ServiceRequest, ServiceResponse> mapper,
            IMapper<UserBonus, BonusActivationRequest, UserBonusResponse> userBonusMapper)
        {
            _logger = logger;
            _repository = repository;
            _userBonusRepository = userBonusRepository;
            _mapper = mapper;
            _userBonusMapper = userBonusMapper;
        }

        [HttpGet("{skip}/{take}/{searchQuery?}")]
        public async Task<IActionResult> Get(int skip, int take, string searchQuery = null)
        {
            IReadOnlyList<Service> priceLists =
                await _repository.GetAsync(new ServiceSpecification(skip, take, searchQuery));

            if (priceLists.Any())
            {
                PaginatedResponse<ServiceResponse> response = new PaginatedResponse<ServiceResponse>
                {
                    Data = priceLists.Select(ls => _mapper.Map(ls)),
                    Total = await _repository.CountAsync(new ServiceSpecification(searchQuery))
                };

                return Ok(response);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Service service = await _repository.GetByIdAsync(id);

            if (service != null)
            {
                return Ok(_mapper.Map(service));
            }

            return StatusCode(StatusCodes.Status204NoContent);

        }

        [HttpPost]
        public async Task<IActionResult> CreateService(ServiceRequest request)
        {
            try
            {
                Service service = _mapper.Map(request);

                service = await _repository.AddAsync(service);

                return CreatedAtAction(nameof(GetById), new { id = service.Id }, _mapper.Map(service));
            }

            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}/activate-bonus")]
        public async Task<IActionResult> Put(Guid id, BonusActivationRequest request)
        {
            try
            {
                // Check if service is still available.
                Service service = await _repository.GetByIdAsync(id);

                if (service == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                UserBonus userBonus = _userBonusMapper.Map(request);

                await _userBonusRepository.AddAsync(userBonus);

                return Ok();
            }

            catch (Exception e)
            {
                _logger.LogError(e.Message, id, request);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
