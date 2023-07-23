using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Departments.Commands.RemoveById;

public sealed class RemoveDepartmentByIdCommandValidator : AbstractValidator<RemoveDepartmentByIdCommand>
{
    public RemoveDepartmentByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}