using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;

public record GetCompanyByIdQuery(
    Guid Id) : IRequest<Result<GetCompanyByIdModel>>;
