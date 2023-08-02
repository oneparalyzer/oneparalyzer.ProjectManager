using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.RemoveById;

public sealed class RemoveProjectTaskByIdCommandValidator : AbstractValidator<RemoveProjectTaskByIdCommand>
{
    public RemoveProjectTaskByIdCommandValidator()
    {
        RuleFor(x => x.ProjectId).NotEqual(Guid.Empty);
        RuleFor(x => x.ProjectTaskId).NotEqual(Guid.Empty);
    }
}
