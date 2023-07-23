using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Commands.Update;

public sealed class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<UpdateCompanyCommand> _validator;
    private readonly ILogger<UpdateCompanyCommandHandler> _logger;

    public UpdateCompanyCommandHandler(
        IApplicationDbContext context,
        IValidator<UpdateCompanyCommand> validator,
        ILogger<UpdateCompanyCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<SimpleResult> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var result = new SimpleResult();

        try
        {
            var validationResult = 
                await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }

            Company? existingCompany = await _context.Companies
                .FirstOrDefaultAsync(x =>
                    x.Id != CompanyId.Create(request.Id) &&
                    x.Title == request.NewTitle,
                    cancellationToken);
            if (existingCompany is not null)
            {
                result.AddError("Компания с таким названием уже существует.");
            }

            existingCompany = await _context.Companies
                .FirstOrDefaultAsync(x =>
                    x.Id == CompanyId.Create(request.Id),
                    cancellationToken);
            if (existingCompany is null)
            {
                result.AddError("Компания не найдена.");
            }

            if (!result.Succeed)
            {
                return result;
            }
            
            existingCompany.Update(request.NewTitle);
            
            _context.Companies.Update(existingCompany);
            await _context.SaveChangesAsync(cancellationToken);

            return result;

        }
        catch (Exception ex)
        {
            result.AddError("Произошла ошибка.");
            _logger.LogError(ex.Message);
            return result;
        }
    }
}
