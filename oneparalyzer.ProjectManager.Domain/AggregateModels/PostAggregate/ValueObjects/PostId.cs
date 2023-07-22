using oneparalyzer.ProjectManager.Domain.Common.ValueObjects;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;

public sealed class PostId : Id
{
    private PostId(Guid value) : base(value) { }

    public static PostId Create()
    {
        return new PostId(Guid.NewGuid());
    }

    public static PostId Create(Guid value)
    {
        return new PostId(value);
    }
}
