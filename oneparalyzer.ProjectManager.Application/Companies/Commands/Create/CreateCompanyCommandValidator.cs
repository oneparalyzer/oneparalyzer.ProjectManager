using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Companies.Commands.Create;

public sealed class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Title).MinimumLength(5).MaximumLength(50);
    }
}
