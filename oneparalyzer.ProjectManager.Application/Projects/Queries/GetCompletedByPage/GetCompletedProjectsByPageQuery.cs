using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Projects.Queries.GetCompletedByPage;

public record GetCompletedProjectsByPageQuery(
    int PageSize,
    int PageNumber,
    Guid DepartmentId) : IRequest<Result<IEnumerable<GetCompletedProjectsByPageModel>>>;