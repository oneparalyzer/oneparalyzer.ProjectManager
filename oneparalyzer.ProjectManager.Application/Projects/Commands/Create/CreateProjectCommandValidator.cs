using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Projects.Commands.Create;

public sealed class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.EmployeeId).NotEqual(Guid.Empty);
        RuleFor(x => x.Title).MinimumLength(5).MaximumLength(50);
    }
}
