using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Departments.Commands.Update;

public sealed class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<UpdateDepartmentCommand> _validator;
    private readonly ILogger<UpdateDepartmentCommandHandler> _logger;

    public UpdateDepartmentCommandHandler(
        IApplicationDbContext context,
        IValidator<UpdateDepartmentCommand> validator,
        ILogger<UpdateDepartmentCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<SimpleResult> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
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

            Department? existingDepartment = await _context.Departments
                .FirstOrDefaultAsync(x =>
                        x.Id == DepartmentId.Create(request.Id),
                    cancellationToken);
            if (existingDepartment is null)
            {
                result.AddError("Структурное подразделение не найдено");
                return result;
            }
            
            existingDepartment.Update(request.NewTitle);
            _context.Departments.Update(existingDepartment);
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