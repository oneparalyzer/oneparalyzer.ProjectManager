using oneparalyzer.ProjectManager.Domain.Common.ValueObjects;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;

public sealed class EmployeeId : Id
{
    private EmployeeId(Guid value) : base(value) { }

    public static EmployeeId Create()
    {
        return new EmployeeId(Guid.NewGuid());
    }

    public static EmployeeId Create(Guid value)
    {
        return new EmployeeId(value);
    }
}
