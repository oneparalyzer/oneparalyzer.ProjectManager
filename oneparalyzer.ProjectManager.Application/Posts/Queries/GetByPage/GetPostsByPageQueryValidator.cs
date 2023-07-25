using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Posts.Queries.GetByPage;

public sealed class GetPostsByPageQueryValidator : AbstractValidator<GetPostsByPageQuery>
{
    public GetPostsByPageQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x => x.DepartmentId).NotEqual(Guid.Empty);
    }
}
