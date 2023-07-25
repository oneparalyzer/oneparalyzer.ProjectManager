using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Posts.Queries.GetByPage;

public record GetPostsByPageQuery(
    int PageSize,
    int PageNumber,
    Guid DepartmentId) : IRequest<Result<IEnumerable<GetPostsByPageModel>>>;
