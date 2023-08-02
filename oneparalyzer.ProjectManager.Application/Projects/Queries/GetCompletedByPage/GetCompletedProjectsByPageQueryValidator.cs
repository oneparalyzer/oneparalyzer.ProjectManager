using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Projects.Queries.GetCompletedByPage;

public class GetCompletedProjectsByPageQueryValidator : AbstractValidator<GetCompletedProjectsByPageQuery>
{
    public GetCompletedProjectsByPageQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.DepartmentId).NotEqual(Guid.Empty);
    }
}