using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Departments.Queries.GetByPage;

public sealed class GetDepartmentsByPageQueryValidator : AbstractValidator<GetDepartmentsByPageQuery>
{
    public GetDepartmentsByPageQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.OfficeId).NotEqual(Guid.Empty);
    }
}