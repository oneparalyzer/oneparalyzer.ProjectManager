using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Commands.RemoveById;

public record RemoveCompanyByIdCommand(
    Guid Id) : IRequest<SimpleResult>;
