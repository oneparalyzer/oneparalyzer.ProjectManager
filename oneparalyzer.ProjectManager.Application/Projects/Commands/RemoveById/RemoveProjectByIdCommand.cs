using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Projects.Commands.RemoveById;

public record RemoveProjectByIdCommand(
    Guid Id) : IRequest<SimpleResult>;
