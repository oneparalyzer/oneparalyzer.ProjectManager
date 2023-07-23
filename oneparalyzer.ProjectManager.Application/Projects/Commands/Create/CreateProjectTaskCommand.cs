namespace oneparalyzer.ProjectManager.Application.Projects.Commands.Create;

public record CreateProjectTaskCommand(
    int Number,
    string Description,
    bool IsCompleted,
    Guid ResponsibleEmployeeId);
