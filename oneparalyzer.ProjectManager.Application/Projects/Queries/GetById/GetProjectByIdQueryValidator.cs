using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Projects.Queries.GetById;

public sealed class GetProjectByIdQueryValidator : AbstractValidator<GetProjectByIdQuery>
{
    public GetProjectByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}
