using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Offices.Commands.RemoveById;

public sealed class RemoveOfficeByIdCommandValidator : AbstractValidator<RemoveOfficeByIdCommand>
{
    public RemoveOfficeByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}
