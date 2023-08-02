using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.SeedWorks;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate;

public sealed class Office : AggregateRoot<OfficeId>
{
    public Office(OfficeId id, string title, CompanyId companyId) : base(id)
    {
        Title = title;
        CompanyId = companyId;
    }

    private Office(OfficeId id) : base(id) { }

    public string Title { get; private set; }
    public CompanyId CompanyId { get; private set; }

    public void Update(string newTitle)
    {
        Title = newTitle;
    }
}
