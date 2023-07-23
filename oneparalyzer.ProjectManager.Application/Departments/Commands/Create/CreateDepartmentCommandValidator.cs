using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Departments.Commands.Create;

public sealed class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Title).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.OfficeId).NotEqual(Guid.Empty);
    }
}