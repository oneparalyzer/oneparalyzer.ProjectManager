using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Employees.Commands.Create;

public record CreateEmployeeCommand(
    string Surname,
    string Name,
    string Patronymic,
    Guid UserId,
    Guid PostId) : IRequest<SimpleResult>;