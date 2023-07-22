using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.Entities;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;

namespace oneparalyzer.ProjectManager.Infrastructure.Persistance.EntityTypeConfigurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => ProjectId.Create(value));

        builder.Property(x => x.EmployeeId)
            .HasConversion(
                id => id.Value,
                value => EmployeeId.Create(value));

        builder.Property(x => x.Title).IsRequired();

        builder.HasMany(x => x.ProjectTasks).WithOne();
    }
}
