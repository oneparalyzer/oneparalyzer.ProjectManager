using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Departments.Queries.GetByPage;

public record GetDepartmentsByPageQuery(
    int PageSize,
    int PageNumber,
    Guid OfficeId) : IRequest<Result<IEnumerable<GetDepartmentsByPageModel>>>;