using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Projects.Commands.Create;

public record CreateProjectCommand(
    string Title,
    Guid EmployeeId,
    List<CreateProjectTaskCommand> ProjectTasks) : IRequest<SimpleResult>;

