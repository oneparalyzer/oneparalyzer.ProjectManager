using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Departments.Queries.GetById;

public record GetDepartmentByIdQuery(
    Guid Id) : IRequest<Result<GetDepartmentByIdModel>>;