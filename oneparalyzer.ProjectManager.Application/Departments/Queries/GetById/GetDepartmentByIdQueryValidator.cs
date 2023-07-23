using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Departments.Queries.GetById;

public sealed class GetDepartmentByIdQueryValidator : AbstractValidator<GetDepartmentByIdQuery>
{
    public GetDepartmentByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}