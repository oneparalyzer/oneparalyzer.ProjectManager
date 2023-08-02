using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Projects.Commands.Update;

public sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.NewTitle).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.NewEmployeeId).NotEqual(Guid.Empty);
    }
}
