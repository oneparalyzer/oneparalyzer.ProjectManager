using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Employees.Queries.GetByPage;

public sealed class GetEmployeesByPageQueryValidator : AbstractValidator<GetEmployeesByPageQuery>
{
    public GetEmployeesByPageQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PostId).NotEqual(Guid.Empty);
    }
}