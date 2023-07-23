using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Offices.Queries.GetByPage;

public sealed class GetOfficesByPageQueryValidator : AbstractValidator<GetOfficesByPageQuery>
{
    public GetOfficesByPageQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.CompanyId).NotEqual(Guid.Empty);
    }
}