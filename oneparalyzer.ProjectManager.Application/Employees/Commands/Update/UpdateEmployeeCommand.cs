using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Employees.Commands.Update;

public record UpdateEmployeeCommand(
    Guid Id,
    string Surname,
    string Name,
    string Patronymic,
    Guid UserId,
    Guid PostId) : IRequest<SimpleResult>;