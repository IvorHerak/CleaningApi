using System;

namespace CleaningManagement.BLL.CleaningPlans.Models
{
    public class CleaningPlanModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
    }
}
