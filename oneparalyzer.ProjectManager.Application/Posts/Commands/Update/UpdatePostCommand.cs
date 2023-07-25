using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Posts.Commands.Update;

public record UpdatePostCommand(
    Guid Id,
    string NewTitle,
    Guid NewDepartmentId) : IRequest<SimpleResult>;