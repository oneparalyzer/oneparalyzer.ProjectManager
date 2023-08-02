using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Add;

public record AddProjectTaskCommand(
    Guid ProjectId,
    int Number,
    string Description,
    Guid ResponsibleEmployeeId) : IRequest<SimpleResult>;

