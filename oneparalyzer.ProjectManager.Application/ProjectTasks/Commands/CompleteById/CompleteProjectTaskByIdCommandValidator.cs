using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.CompleteById;

public class CompleteProjectTaskByIdCommandValidator : AbstractValidator<CompleteProjectTaskByIdCommand>
{
    public CompleteProjectTaskByIdCommandValidator()
    {
        RuleFor(x => x.ProjectId).NotEqual(Guid.Empty);
        RuleFor(x => x.ProjectTaskId).NotEqual(Guid.Empty);
    }
}