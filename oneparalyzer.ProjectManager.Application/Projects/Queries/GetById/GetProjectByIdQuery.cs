using MediatR;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Projects.Queries.GetById;

public record GetProjectByIdQuery(
    Guid Id) : IRequest<Result<GetProjectByIdModel>>;
