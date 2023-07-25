using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Employees.Queries.GetByPage;

public record GetEmployeesByPageQuery(
    int PageSize,
    int PageNumber,
    Guid PostId) : IRequest<Result<IEnumerable<GetEmployeesByPageModel>>>;