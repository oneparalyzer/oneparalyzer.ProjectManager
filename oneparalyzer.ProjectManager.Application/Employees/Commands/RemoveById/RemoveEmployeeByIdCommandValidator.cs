using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Employees.Commands.RemoveById;

public sealed class RemoveEmployeeByIdCommandValidator : AbstractValidator<RemoveEmployeeByIdCommand>
{
    public RemoveEmployeeByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}