using oneparalyzer.ProjectManager.Domain.Common.ValueObjects;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;

public sealed class ProjectId : Id
{
    private ProjectId(Guid value) : base(value) { }

    public static ProjectId Create()
    {
        return new ProjectId(Guid.NewGuid());
    }

    public static ProjectId Create(Guid value)
    {
        return new ProjectId(value);
    }
}
