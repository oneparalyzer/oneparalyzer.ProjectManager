using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Offices.Queries.GetById;

public record GetOfficeByIdQuery(
    Guid Id) : IRequest<Result<GetOfficeByIdModel>>;