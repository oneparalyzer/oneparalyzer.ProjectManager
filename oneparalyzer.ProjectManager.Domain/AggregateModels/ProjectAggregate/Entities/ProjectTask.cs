using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.SeedWorks;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.Entities;

public sealed class ProjectTask : Entity<ProjectTaskId>
{
    public ProjectTask(
        ProjectTaskId id,
        int number,
        string description,
        bool isCompleted,
        EmployeeId responsibleEmployeeId) : base(id)
    {
        Number = number;
        Description = description;
        IsCompleted = isCompleted;
        ResponsibleEmployeeId = responsibleEmployeeId;
    }

    public int Number { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public EmployeeId ResponsibleEmployeeId { get; private set; }

    public void Complete()
    {
        IsCompleted = true;
    }
}
