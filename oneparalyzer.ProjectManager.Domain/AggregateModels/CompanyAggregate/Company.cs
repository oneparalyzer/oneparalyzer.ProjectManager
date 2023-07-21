using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.SeedWorks;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate;

public sealed class Company : AggregateRoot<CompanyId>
{
    public Company(CompanyId id, string title) : base(id)
    {
        Title = title;
    }

    public string Title { get; private set; }
}
