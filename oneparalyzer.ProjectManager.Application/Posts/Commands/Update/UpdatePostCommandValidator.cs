using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Posts.Commands.Update;

public sealed class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.NewTitle).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.NewDepartmentId).NotEqual(Guid.Empty);
    }
}