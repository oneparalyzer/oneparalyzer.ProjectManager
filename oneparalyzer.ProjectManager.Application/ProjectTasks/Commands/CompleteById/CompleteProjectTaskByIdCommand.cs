using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.CompleteById;

public record CompleteProjectTaskByIdCommand(
    Guid ProjectId,
    Guid ProjectTaskId) : IRequest<SimpleResult>;