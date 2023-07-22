using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Commands.Create;

public sealed class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CreateCompanyCommand> _validator;
    private readonly ILogger<CreateCompanyCommandHandler> _logger;

    public CreateCompanyCommandHandler(
        IApplicationDbContext context,
        IValidator<CreateCompanyCommand> validator,
        ILogger<CreateCompanyCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<SimpleResult> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var result = new SimpleResult();

        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
        {
            result.AddValidationErrors(validationResult.ToDictionary());
        }

        try
        {
            Company? company = await _context.Companies
            .FirstOrDefaultAsync(x =>
                x.Title == request.Title,
                cancellationToken);
            if (company is not null)
            {
                result.AddError("Компания с таким названием уже существует.");
                return result;
            }

            company = new Company(
                CompanyId.Create(),
                request.Title);

            await _context.Companies.AddAsync(company, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            result.AddError("Произошла ошиюка.");
            _logger.LogError(ex.Message);
            return result;
        }
    }
}
