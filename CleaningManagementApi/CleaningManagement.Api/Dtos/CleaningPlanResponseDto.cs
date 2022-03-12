using System;

namespace CleaningManagement.Api.Dtos
{
    public class CleaningPlanResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
    }
}
