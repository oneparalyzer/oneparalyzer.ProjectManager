using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Offices.Commands.Create;

public sealed class CreateOfficeCommandValidator : AbstractValidator<CreateOfficeCommand>
{
    public CreateOfficeCommandValidator()
    {
        RuleFor(x => x.Title).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.CompanyId).NotEqual(Guid.Empty);
    }
}
