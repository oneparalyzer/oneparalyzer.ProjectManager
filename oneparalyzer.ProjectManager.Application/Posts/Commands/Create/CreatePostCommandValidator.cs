using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Posts.Commands.Create;

public sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.DepartmentId).NotEqual(Guid.Empty);
    }
}