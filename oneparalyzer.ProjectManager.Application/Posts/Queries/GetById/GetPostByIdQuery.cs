using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Posts.Queries.GetById;

public record GetPostByIdQuery(
    Guid Id) : IRequest<Result<GetPostByIdModel>>;
