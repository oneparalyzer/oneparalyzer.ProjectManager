using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Departments.Commands.Update;

public sealed class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.NewTitle).MinimumLength(5).MaximumLength(50);
    }
}