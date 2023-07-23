using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Offices.Commands.RemoveById;

public record RemoveOfficeByIdCommand(
    Guid Id) : IRequest<SimpleResult>;
