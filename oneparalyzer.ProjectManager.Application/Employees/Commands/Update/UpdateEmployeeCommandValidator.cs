using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Employees.Commands.Update;

public sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Name).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.Surname).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.Patronymic).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.PostId).NotEqual(Guid.Empty);
    }
}