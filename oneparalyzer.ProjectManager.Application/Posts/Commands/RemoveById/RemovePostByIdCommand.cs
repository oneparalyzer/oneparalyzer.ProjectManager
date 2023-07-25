using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Posts.Commands.RemoveById;

public record RemovePostByIdCommand(
    Guid Id) : IRequest<SimpleResult>;