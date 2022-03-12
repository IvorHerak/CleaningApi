using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleaningManagement.DAL.Entities.Configurations
{
    public class CleaningPlanConfiguration : IEntityTypeConfiguration<CleaningPlan>
    {
        public void Configure(EntityTypeBuilder<CleaningPlan> builder)
        {
            builder
                .HasKey(cp => cp.Id);

            builder
                .Property(cp => cp.Title)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .Property(cp => cp.Description)
                .HasMaxLength(512);
        }
    }
}
