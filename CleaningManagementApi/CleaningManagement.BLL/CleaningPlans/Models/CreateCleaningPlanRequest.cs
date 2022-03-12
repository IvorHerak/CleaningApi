namespace CleaningManagement.BLL.CleaningPlans.Models
{
    public class CreateCleaningPlanRequest
    {
        public string Title { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
    }
}
