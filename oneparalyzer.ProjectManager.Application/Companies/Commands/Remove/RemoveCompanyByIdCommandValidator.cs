using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Companies.Commands.Remove;

public sealed class RemoveCompanyByIdCommandValidator : AbstractValidator<RemoveCompanyByIdCommand>
{
    public RemoveCompanyByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}
