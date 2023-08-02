using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Departments.Commands.Update;

public record UpdateDepartmentCommand(
    Guid Id,
    string NewTitle) : IRequest<SimpleResult>;