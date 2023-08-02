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

namespace oneparalyzer.ProjectManager.Application.Offices.Commands.Update;

public sealed class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<UpdateOfficeCommand> _validator;
    private readonly ILogger<UpdateOfficeCommandHandler> _logger;

    public UpdateOfficeCommandHandler(
        IApplicationDbContext context,
        IValidator<UpdateOfficeCommand> validator,
        ILogger<UpdateOfficeCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<SimpleResult> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
    {
        var result = new SimpleResult();

        try
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }

            Office? existingOffice = await _context.Offices
                .FirstOrDefaultAsync(x =>
                        x.Id == OfficeId.Create(request.Id),
                    cancellationToken);
            if (existingOffice is null)
            {
                result.AddError("Офис не найден.");
                return result;
            }
            
            existingOffice.Update(request.NewTitle);

            _context.Offices.Update(existingOffice);
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
