using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Departments.Commands.RemoveById;

public record RemoveDepartmentByIdCommand(
    Guid Id) : IRequest<SimpleResult>;