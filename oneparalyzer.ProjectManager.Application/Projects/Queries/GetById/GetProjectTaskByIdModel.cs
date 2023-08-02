namespace oneparalyzer.ProjectManager.Application.Projects.Queries.GetById;

public record GetProjectTaskByIdModel(
    Guid Id,
    int Number,
    string Description,
    bool IsCompleted,
    Guid ResponsibleEmployeeId);