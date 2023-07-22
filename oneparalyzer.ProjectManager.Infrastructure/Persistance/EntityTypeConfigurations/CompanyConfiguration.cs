using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate;

namespace oneparalyzer.ProjectManager.Infrastructure.Persistance.EntityTypeConfigurations;

public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value, 
                value => CompanyId.Create(value));

        builder.Property(x => x.Title).HasMaxLength(50).IsRequired();

        builder.HasIndex(x => x.Title);

        builder.HasMany<Office>().WithOne().HasForeignKey(x => x.CompanyId);
    }
}
