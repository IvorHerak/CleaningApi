using CleaningManagement.Api.Dtos;
using CleaningManagement.Api.Mappers;
using CleaningManagement.BLL.CleaningPlans.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningManagement.Api.Controllers
{
    [ApiController]
    [Route("api/cleaningplans")]
    public class CleaningPlansController : ControllerBase
    {
        private const string CleaningPlanGetByIdEndpointName = "CleaningPlanGetEndpoint";
        private readonly ICleaningPlanService cleaningPlanService;
        private readonly ICleaningPlanMapper cleaningPlanMapper;

        public CleaningPlansController
        (
            ICleaningPlanService cleaningPlanService,
            ICleaningPlanMapper cleaningPlanMapper
        )
        {
            this.cleaningPlanService = cleaningPlanService;
            this.cleaningPlanMapper = cleaningPlanMapper;
        }

        [HttpGet("{id}", Name = CleaningPlanGetByIdEndpointName)]
        public async Task<IActionResult> GetCleaningPlanAsync([FromRoute] Guid id)
        {
            var cleaningPlan = await cleaningPlanService.GetCleaningPlanAsync(id);
            var response = cleaningPlanMapper.MapToCleaningPlanResponseDto(cleaningPlan);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCleaningPlansAsync([FromQuery] int customerId)
        {
            var cleaningPlans = await cleaningPlanService.GetCleaningPlansForCustomerAsync(customerId);
            var response = cleaningPlans.Select(cleaningPlanMapper.MapToCleaningPlanResponseDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCleaningPlanAsync(CreateCleaningPlanRequestDto request)
        {
            var businessRequest = cleaningPlanMapper.MapToCreateCleaningPlanRequest(request);
            var cleaningPlan = await cleaningPlanService.CreateCleaningPlanAsync(businessRequest);
            var response = cleaningPlanMapper.MapToCleaningPlanResponseDto(cleaningPlan);
            return CreatedAtRoute(CleaningPlanGetByIdEndpointName, new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCleaningPlanAsync([FromRoute] Guid id, [FromBody] UpdateCleaningPlanRequestDto request)
        {
            var businessRequest = cleaningPlanMapper.MapToUpdateCleaningPlanRequest(request, id);
            var cleaningPlan = await cleaningPlanService.UpdateCleaningPlanAsync(businessRequest);
            var response = cleaningPlanMapper.MapToCleaningPlanResponseDto(cleaningPlan);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCleaningPlanAsync([FromRoute] Guid id)
        {
            await cleaningPlanService.DeleteCleaningPlanAsync(id);
            return NoContent();
        }
    }
}