using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Offices.Queries.GetByPage;

public sealed class GetOfficeByPageQueryValidator : AbstractValidator<GetOfficesByPageQuery>
{
    public GetOfficeByPageQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.CompanyId).NotEqual(Guid.Empty);
    }
}