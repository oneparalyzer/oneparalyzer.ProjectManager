using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Application.Companies.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Offices.Commands.Create;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Offices.Commands.RemoveById;

public sealed class RemoveOfficeByIdCommandHandler : IRequestHandler<RemoveOfficeByIdCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RemoveOfficeByIdCommand> _validator;
    private readonly ILogger<RemoveCompanyByIdCommandHandler> _logger;

    public RemoveOfficeByIdCommandHandler(
        IApplicationDbContext context,
        IValidator<RemoveOfficeByIdCommand> validator,
        ILogger<RemoveCompanyByIdCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<SimpleResult> Handle(RemoveOfficeByIdCommand request, CancellationToken cancellationToken)
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

            Office? existingOffice = await _context.Offices
                .FirstOrDefaultAsync(x =>
                    x.Id == OfficeId.Create(request.Id),
                    cancellationToken);
            if (existingOffice is null)
            {
                result.AddError("Офис не найден.");
                return result;
            }

            _context.Offices.Remove(existingOffice);
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
