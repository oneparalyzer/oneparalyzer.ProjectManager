using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Posts.Commands.Create;

public record CreatePostCommand(
    string Title,
    Guid DepartmentId) : IRequest<SimpleResult>;