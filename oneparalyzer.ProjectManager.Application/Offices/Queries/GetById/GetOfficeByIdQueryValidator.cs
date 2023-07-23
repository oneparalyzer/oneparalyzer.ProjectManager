using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Offices.Queries.GetById;

public sealed class GetOfficeByIdQueryValidator : AbstractValidator<GetOfficeByIdQuery>
{
    public GetOfficeByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}