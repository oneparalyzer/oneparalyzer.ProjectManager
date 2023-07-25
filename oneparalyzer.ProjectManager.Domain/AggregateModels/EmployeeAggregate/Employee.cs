using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.ValueObjects;
using oneparalyzer.ProjectManager.Domain.SeedWorks;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate;

public sealed class Employee : AggregateRoot<EmployeeId>
{
    private Employee(EmployeeId id) : base(id) { }

    public Employee(EmployeeId id, FullName fullName, PostId postId, UserId userId) : base(id)
    {
        FullName = fullName;
        PostId = postId;
        UserId = userId;
    }

    public FullName FullName { get; private set; }
    public PostId PostId { get; private set; }
    public UserId UserId { get; private set; }

    public void Update(FullName newFullName, PostId newPostId, UserId newUserId)
    {
        FullName = newFullName;
        PostId = newPostId;
        UserId = newUserId;
    }
}
