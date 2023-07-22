using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;

public sealed class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
{
    public GetCompanyByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}
