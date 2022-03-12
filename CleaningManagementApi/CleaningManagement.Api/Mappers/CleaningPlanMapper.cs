using CleaningManagement.Api.Dtos;
using CleaningManagement.BLL.CleaningPlans.Models;
using System;

namespace CleaningManagement.Api.Mappers
{
    public class CleaningPlanMapper : ICleaningPlanMapper
    {
        public CleaningPlanResponseDto MapToCleaningPlanResponseDto(CleaningPlanModel model)
        {
            var result = new CleaningPlanResponseDto
            {
                Id = model.Id,
                Title = model.Title,
                CreatedAt = model.CreationDate,
                CustomerId = model.CustomerId,
                Description = model.Description
            };
            return result;
        }

        public CreateCleaningPlanRequest MapToCreateCleaningPlanRequest(CreateCleaningPlanRequestDto request)
        {
            var result = new CreateCleaningPlanRequest
            {
                Title = request.Title,
                CustomerId = request.CustomerId,
                Description = request.Description
            };
            return result;
        }

        public UpdateCleaningPlanRequest MapToUpdateCleaningPlanRequest(UpdateCleaningPlanRequestDto request, Guid id)
        {
            var result = new UpdateCleaningPlanRequest
            {
                Id = id,
                Title = request.Title,
                CustomerId = request.CustomerId,
                Description = request.Description
            };
            return result;
        }
    }
}
