using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.Entities;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;

namespace oneparalyzer.ProjectManager.Infrastructure.Persistance.EntityTypeConfigurations;

public sealed class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => ProjectTaskId.Create(value));

        builder.Property(x => x.ResponsibleEmployeeId)
            .HasConversion(
                id => id.Value,
                value => EmployeeId.Create(value));

        builder.Property(x => x.Number).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(400).IsRequired();

        
    }
}
