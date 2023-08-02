using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Update;

public record UpdateProjectTaskCommand(
    Guid ProjectId,
    Guid ProjectTaskId,
    int NewNumber,
    string NewDescription,
    Guid NewResponsibleEmployeeId) : IRequest<SimpleResult>;
