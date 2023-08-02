using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.Entities;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;
using oneparalyzer.ProjectManager.Domain.SeedWorks;

namespace oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;

public sealed class Project : AggregateRoot<ProjectId>
{
    private readonly List<ProjectTask> _projectTasks = new();

    private Project(ProjectId id) : base(id) { }

    public Project(ProjectId id, string title, EmployeeId employeeId, List<ProjectTask> projectTasks) : base(id)
    {
        _projectTasks = projectTasks;
        Title = title;
        EmployeeId = employeeId;
    }

    public string Title { get; private set; }
    public EmployeeId EmployeeId { get; private set; }
    public DateTime DateStart { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; private set; }

    public IReadOnlyList<ProjectTask> ProjectTasks => _projectTasks.AsReadOnly();

    public void Update(string newTitle)
    {
        Title = newTitle;
    }

    public SimpleResult AddProjectTask(ProjectTask projectTask)
    {
        var result = new SimpleResult();

        ProjectTask? existingProjectTask =
            _projectTasks.FirstOrDefault(x => x.Id == projectTask.Id);
        if (existingProjectTask is not null)
        {
            result.AddError("Задача уже существует.");
            return result;
        }

        int MaxNumberProjectTask =
            _projectTasks.Select(x => x.Number).Max();
        if (projectTask.Number - 1 != MaxNumberProjectTask)
        {
            result.AddError("Не корректный номер задачи.");
            return result;
        }

        _projectTasks.Add(projectTask);

        return result;
    }

    public SimpleResult RemoveProjectTaskById(ProjectTaskId projectTaskId)
    {
        var result = new SimpleResult();

        ProjectTask? existingProjectTask = _projectTasks.FirstOrDefault(x => x.Id == projectTaskId);
        if (existingProjectTask is null)
        {
            result.AddError("Задача не найдена.");
            return result;
        }

        _projectTasks.Remove(existingProjectTask);

        return result;
    }
    
    public SimpleResult UpdateProjectTask(ProjectTask newProjectTask)
    {
        var result = new SimpleResult();

        ProjectTask? existingProjectTask = _projectTasks
            .FirstOrDefault(x => 
                x.Id == newProjectTask.Id);
        if (existingProjectTask is null)
        {
            result.AddError("Задача не найдена.");
            return result;
        }
        
        UpdatedDate = DateTime.UtcNow;
        return result;
    }

    public SimpleResult CompleteProjectTaskById(ProjectTaskId projectTaskId)
    {
        var result = new SimpleResult();

        ProjectTask? existingProjectTask = _projectTasks
            .FirstOrDefault(x => 
                x.Id == projectTaskId);
        if (existingProjectTask is null)
        {
            result.AddError("Задача не найдена.");
            return result;
        }

        

        return result;
    }

    public bool IsCompleted()
    {
        ProjectTask? notCompletedProjectTask = _projectTasks
            .FirstOrDefault(x =>
                x.IsCompleted == false);
        if (notCompletedProjectTask is null)
        {
            return true;
        }

        return false;
    }
}
