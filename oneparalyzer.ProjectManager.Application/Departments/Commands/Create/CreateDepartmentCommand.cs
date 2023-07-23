using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Departments.Commands.Create;

public record CreateDepartmentCommand(
    string Title,
    Guid OfficeId) : IRequest<SimpleResult>;