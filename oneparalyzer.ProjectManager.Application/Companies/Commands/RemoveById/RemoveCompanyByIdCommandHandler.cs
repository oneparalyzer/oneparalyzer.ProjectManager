using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Commands.RemoveById;

public sealed class RemoveCompanyByIdCommandHandler : IRequestHandler<RemoveCompanyByIdCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RemoveCompanyByIdCommand> _validator;
    private readonly ILogger<RemoveCompanyByIdCommandHandler> _logger;

    public RemoveCompanyByIdCommandHandler(
        IApplicationDbContext context,
        IValidator<RemoveCompanyByIdCommand> validator,
        ILogger<RemoveCompanyByIdCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<SimpleResult> Handle(RemoveCompanyByIdCommand request, CancellationToken cancellationToken)
    {
        var result = new SimpleResult();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }

            Company? company = await _context.Companies
                .FirstOrDefaultAsync(x => 
                    x.Id == CompanyId.Create(request.Id),
                    cancellationToken);
            if (company is null)
            {
                result.AddError("Офис не найден.");
                return result;
            }

            _context.Companies.Remove(company);
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
