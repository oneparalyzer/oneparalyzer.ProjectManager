using oneparalyzer.ProjectManager.Domain.Common.ValueObjects;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate.ValueObjects;

public sealed class DepartmentId : Id
{
    private DepartmentId(Guid value) : base(value) { }

    public static DepartmentId Create()
    {
        return new DepartmentId(Guid.NewGuid());
    }

    public static DepartmentId Create(Guid value)
    {
        return new DepartmentId(value);
    }
}
