using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Offices.Commands.Update;

public record UpdateOfficeCommand(
    Guid Id,
    string NewTitle,
    Guid NewCompanyId) : IRequest<SimpleResult>;
