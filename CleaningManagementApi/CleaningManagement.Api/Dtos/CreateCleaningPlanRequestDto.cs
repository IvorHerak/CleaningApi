﻿namespace CleaningManagement.Api.Dtos
{
    public class CreateCleaningPlanRequestDto
    {
        public string Title { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
    }
}
