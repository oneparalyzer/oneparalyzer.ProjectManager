using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Projects.Commands.RemoveById;

public sealed class RemoveProjectByIdCommandValidator : AbstractValidator<RemoveProjectByIdCommand>
{
    public RemoveProjectByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}
