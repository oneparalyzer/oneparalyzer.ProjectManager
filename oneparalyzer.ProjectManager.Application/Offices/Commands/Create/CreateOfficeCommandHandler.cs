using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Offices.Commands.Create;

public sealed class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CreateOfficeCommand> _validator;
    private readonly ILogger<CreateOfficeCommandHandler> _logger;

    public CreateOfficeCommandHandler(
        IApplicationDbContext context,
        IValidator<CreateOfficeCommand> validator,
        ILogger<CreateOfficeCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<SimpleResult> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
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

            Company? existingCompany = await _context.Companies
                .FirstOrDefaultAsync(x =>
                    x.Id == CompanyId.Create(request.CompanyId),
                    cancellationToken);
            if (existingCompany is null)
            {
                result.AddError("Компания не найдена.");
                return result;
            }

            var office = new Office(
                OfficeId.Create(),
                request.Title,
                CompanyId.Create(request.CompanyId));

            await _context.Offices.AddAsync(office, cancellationToken);
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
