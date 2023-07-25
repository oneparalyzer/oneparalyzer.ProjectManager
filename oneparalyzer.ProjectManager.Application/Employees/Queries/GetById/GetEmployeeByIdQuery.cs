using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Employees.Queries.GetById;

public record GetEmployeeByIdQuery(
    Guid Id) : IRequest<Result<GetEmployeeByIdModel>>;