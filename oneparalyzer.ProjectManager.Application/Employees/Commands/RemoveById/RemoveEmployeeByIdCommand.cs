using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Employees.Commands.RemoveById;

public record RemoveEmployeeByIdCommand(
    Guid Id) : IRequest<SimpleResult>;