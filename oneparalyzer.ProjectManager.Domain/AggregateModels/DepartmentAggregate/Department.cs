using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.SeedWorks;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate;

public sealed class Department : AggregateRoot<DepartmentId>
{
    private Department(DepartmentId id) : base(id) { }

    public Department(DepartmentId id, string title, OfficeId officeId) : base(id)
    {
        Title = title;
        OfficeId = officeId;
    }

    public string Title { get; private set; }
    public OfficeId OfficeId { get; private set; }
}
