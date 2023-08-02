using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.RemoveById;

public record RemoveProjectTaskByIdCommand(
    Guid ProjectId,
    Guid ProjectTaskId) : IRequest<SimpleResult>;
