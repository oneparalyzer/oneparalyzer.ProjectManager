using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Companies.Queries.GetByPage;

public sealed class GetCompaniesByPageQueryValidator : AbstractValidator<GetCompaniesByPageQuery>
{
    public GetCompaniesByPageQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
    }
}
