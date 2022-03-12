using CleaningManagement.BLL.CleaningPlans.Models;
using CleaningManagement.BLL.Exceptions;
using CleaningManagement.DAL.Entities;
using CleaningManagement.DAL.Entities.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningManagement.BLL.CleaningPlans.Services
{
    public class CleaningPlanService : ICleaningPlanService
    {
        private readonly IRepository<CleaningPlan> cleaningPlanRepository;
        private readonly IValidator<CreateCleaningPlanRequest> createRequestValidator;
        private readonly IValidator<UpdateCleaningPlanRequest> updateRequestValidator;

        public CleaningPlanService
        (
            IRepository<CleaningPlan> cleaningPlanRepository,
            IValidator<CreateCleaningPlanRequest> createRequestValidator,
            IValidator<UpdateCleaningPlanRequest> updateRequestValidator
        )
        {
            this.cleaningPlanRepository = cleaningPlanRepository;
            this.createRequestValidator = createRequestValidator;
            this.updateRequestValidator = updateRequestValidator;
        }

        public async Task<CleaningPlanModel> GetCleaningPlanAsync(Guid id)
        {
            var cleaningPlan = await cleaningPlanRepository.GetOneAsync
            (
                filter: cp => cp.Id == id,
                disableTracking: true
            );

            if (cleaningPlan == null)
            {
                throw new KeyNotFoundException();
            }

            var response = MapToCleaningPlanModel(cleaningPlan);
            return response;
        }

        public async Task<IEnumerable<CleaningPlanModel>> GetCleaningPlansForCustomerAsync(int customerId)
        {
            var customerCleaningPlans = await cleaningPlanRepository.GetAsync
            (
                filter: cp => cp.CustomerId == customerId,
                disableTracking: true
            );

            var response = customerCleaningPlans.Select(MapToCleaningPlanModel);
            return response;
        }

        public async Task<CleaningPlanModel> CreateCleaningPlanAsync(CreateCleaningPlanRequest request)
        {
            var validationResult = createRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult);
            }

            var cleaningPlan = new CleaningPlan
            {
                Title = request.Title,
                CustomerId = request.CustomerId,
                Description = request.Description,
                CreationDate = DateTime.UtcNow
            };
            cleaningPlanRepository.Add(cleaningPlan);

            await cleaningPlanRepository.SaveAsync();

            var response = MapToCleaningPlanModel(cleaningPlan);
            return response;
        }

        public async Task<CleaningPlanModel> UpdateCleaningPlanAsync(UpdateCleaningPlanRequest request)
        {
            var validationResult = updateRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult);
            }

            var cleaningPlan = await cleaningPlanRepository.GetOneAsync
            (
                filter: cp => cp.Id == request.Id,
                disableTracking: true
            );
            if (cleaningPlan == null)
            {
                throw new KeyNotFoundException();
            }

            cleaningPlan.Title = request.Title;
            cleaningPlan.CustomerId = request.CustomerId;
            cleaningPlan.Description = request.Description;
            cleaningPlanRepository.Update(cleaningPlan);

            await cleaningPlanRepository.SaveAsync();

            var response = MapToCleaningPlanModel(cleaningPlan);
            return response;
        }

        public async Task DeleteCleaningPlanAsync(Guid id)
        {
            var cleaningPlan = await cleaningPlanRepository.GetOneAsync
            (
                filter: cp => cp.Id == id,
                disableTracking: true
            );
            if (cleaningPlan == null)
            {
                throw new KeyNotFoundException();
            }

            cleaningPlanRepository.Delete(cleaningPlan);

            await cleaningPlanRepository.SaveAsync();
        }

        private CleaningPlanModel MapToCleaningPlanModel(CleaningPlan entity)
        {
            var result = new CleaningPlanModel
            {
                Id = entity.Id,
                Title = entity.Title,
                CreationDate = entity.CreationDate,
                CustomerId = entity.CustomerId,
                Description = entity.Description
            };
            return result;
        }
    }
}
