using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Add;

public sealed class AddProjectTaskCommandValidator : AbstractValidator<AddProjectTaskCommand>
{
    public AddProjectTaskCommandValidator()
    {
        RuleFor(x => x.ProjectId).NotEqual(Guid.Empty);
        RuleFor(x => x.Description).MinimumLength(5).MaximumLength(200);
        RuleFor(x => x.Number).GreaterThanOrEqualTo(1);
        RuleFor(x => x.ResponsibleEmployeeId).NotEqual(Guid.Empty);
    }
}
