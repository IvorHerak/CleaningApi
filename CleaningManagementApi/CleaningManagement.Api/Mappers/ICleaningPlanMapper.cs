using CleaningManagement.Api.Dtos;
using CleaningManagement.BLL.CleaningPlans.Models;
using System;

namespace CleaningManagement.Api.Mappers
{
    public interface ICleaningPlanMapper
    {
        public CleaningPlanResponseDto MapToCleaningPlanResponseDto(CleaningPlanModel model);
        public CreateCleaningPlanRequest MapToCreateCleaningPlanRequest(CreateCleaningPlanRequestDto request);
        public UpdateCleaningPlanRequest MapToUpdateCleaningPlanRequest(UpdateCleaningPlanRequestDto request, Guid id);
    }
}