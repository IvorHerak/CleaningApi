using CleaningManagement.BLL.CleaningPlans.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleaningManagement.BLL.CleaningPlans.Services
{
    public interface ICleaningPlanService
    {
        public Task<CleaningPlanModel> CreateCleaningPlanAsync(CreateCleaningPlanRequest request);
        public Task<IEnumerable<CleaningPlanModel>> GetCleaningPlansForCustomerAsync(int customerId);
        public Task<CleaningPlanModel> GetCleaningPlanAsync(Guid id);
        public Task<CleaningPlanModel> UpdateCleaningPlanAsync(UpdateCleaningPlanRequest businessRequest);
        public Task DeleteCleaningPlanAsync(Guid id);
    }
}
