using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Posts.Commands.RemoveById;

public sealed class RemovePostByIdCommandValidator : AbstractValidator<RemovePostByIdCommand>
{
    public RemovePostByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}