using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Offices.Commands.Create;

public record CreateOfficeCommand(
    string Title,
    Guid CompanyId) : IRequest<SimpleResult>;