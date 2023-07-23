using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.Entities;
using oneparalyzer.ProjectManager.Domain.Common.ValueObjects;

namespace oneparalyzer.ProjectManager.Infrastructure.Persistance.EntityTypeConfigurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => EmployeeId.Create(value));

        builder.Property(x => x.PostId)
            .HasConversion(
                id => id.Value,
                value => PostId.Create(value));

        builder.Property(x => x.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.OwnsOne(x => x.FullName)
            .Property(x => x.Surname)
            .HasColumnName("Surname")
            .HasMaxLength(50)
            .IsRequired();

        builder.OwnsOne(x => x.FullName)
            .Property(x => x.Name)
            .HasColumnName("Name")
            .HasMaxLength(50)
            .IsRequired();

        builder.OwnsOne(x => x.FullName)
            .Property(x => x.Patronymic)
            .HasColumnName("Patronymic")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany<Project>().WithOne().HasForeignKey(x => x.EmployeeId);
        builder.HasMany<ProjectTask>().WithOne().HasForeignKey(x => x.ResponsibleEmployeeId);
    }
}
