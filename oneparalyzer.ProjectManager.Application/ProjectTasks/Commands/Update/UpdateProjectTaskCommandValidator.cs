using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Update;

public sealed class UpdateProjectTaskCommandValidator : AbstractValidator<UpdateProjectTaskCommand>
{
    public UpdateProjectTaskCommandValidator()
    {
        RuleFor(x => x.ProjectId).NotEqual(Guid.Empty);
        RuleFor(x => x.NewDescription).MinimumLength(5).MaximumLength(200);
        RuleFor(x => x.NewNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.NewResponsibleEmployeeId).NotEqual(Guid.Empty);
        RuleFor(x => x.ProjectTaskId).NotEqual(Guid.Empty);
    }
}
