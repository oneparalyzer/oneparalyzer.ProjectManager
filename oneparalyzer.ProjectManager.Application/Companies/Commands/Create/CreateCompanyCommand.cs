using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Commands.Create;

public record CreateCompanyCommand(
    string Title) : IRequest<SimpleResult>;
