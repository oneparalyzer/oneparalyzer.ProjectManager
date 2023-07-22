using oneparalyzer.ProjectManager.Domain.Common.ValueObjects;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;

public sealed class ProjectTaskId : Id
{
    private ProjectTaskId(Guid value) : base(value) { }

    public static ProjectTaskId Create()
    {
        return new ProjectTaskId(Guid.NewGuid());
    }

    public static ProjectTaskId Create(Guid value)
    {
        return new ProjectTaskId(value);
    }
}
