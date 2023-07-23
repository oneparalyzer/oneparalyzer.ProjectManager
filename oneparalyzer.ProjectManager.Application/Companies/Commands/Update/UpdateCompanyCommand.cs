using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Commands.Update;

public record UpdateCompanyCommand(
    Guid Id,
    string NewTitle) : IRequest<SimpleResult>;
