using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Commands.Remove;

public record RemoveCompanyByIdCommand(
    Guid Id) : IRequest<SimpleResult>;
