using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.SeedWorks;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate;

public sealed class Post : AggregateRoot<PostId>
{
    public Post(PostId id, string title, DepartmentId departmentId) : base(id)
    {
        Title = title;
        DepartmentId = departmentId;
    }

    private Post(PostId id) : base(id) { }

    public string Title { get; private set; }
    public DepartmentId DepartmentId { get; private set; }

    public void Update(string newTitle)
    {
        Title = newTitle;
    }
}
