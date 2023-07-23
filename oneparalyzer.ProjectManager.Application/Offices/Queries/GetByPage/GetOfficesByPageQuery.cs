using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Offices.Queries.GetByPage;

public record GetOfficesByPageQuery(
    int PageSize,
    int PageNumber,
    Guid CompanyId) : IRequest<Result<IEnumerable<GetOfficesByPageModel>>>;