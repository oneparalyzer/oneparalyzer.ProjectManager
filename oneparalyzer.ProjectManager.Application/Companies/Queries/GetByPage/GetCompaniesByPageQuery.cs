using MediatR;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Queries.GetByPage;

public record GetCompaniesByPageQuery(
    int PageSize,
    int PageNumber) : IRequest<Result<IEnumerable<GetCompaniesByPageModel>>>;
