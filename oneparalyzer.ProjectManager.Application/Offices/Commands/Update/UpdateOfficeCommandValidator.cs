using FluentValidation;

namespace oneparalyzer.ProjectManager.Application.Offices.Commands.Update;

public sealed class UpdateOfficeCommandValidator : AbstractValidator<UpdateOfficeCommand>
{
    public UpdateOfficeCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.NewTitle).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.NewCompanyId).NotEqual(Guid.Empty);
    }
}
