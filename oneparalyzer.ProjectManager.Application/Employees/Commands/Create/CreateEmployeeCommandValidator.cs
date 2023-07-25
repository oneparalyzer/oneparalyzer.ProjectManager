using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Employees.Commands.Create;

public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.Surname).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.Patronymic).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.PostId).NotEqual(Guid.Empty);
    }
}