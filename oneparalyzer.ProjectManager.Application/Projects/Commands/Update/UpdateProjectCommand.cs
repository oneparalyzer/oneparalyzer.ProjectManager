using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Projects.Commands.Update;

public record UpdateProjectCommand(
    Guid Id,
    string  NewTitle,
    Guid NewEmployeeId) : IRequest<SimpleResult>;
