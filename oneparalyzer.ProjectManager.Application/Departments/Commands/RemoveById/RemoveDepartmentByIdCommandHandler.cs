using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Departments.Commands.RemoveById;

public sealed class RemoveDepartmentByIdCommandHandler : IRequestHandler<RemoveDepartmentByIdCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RemoveDepartmentByIdCommand> _validator;
    private readonly ILogger<RemoveDepartmentByIdCommandHandler> _logger;

    public RemoveDepartmentByIdCommandHandler(
        IApplicationDbContext context,
        IValidator<RemoveDepartmentByIdCommand> validator,
        ILogger<RemoveDepartmentByIdCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<SimpleResult> Handle(RemoveDepartmentByIdCommand request, CancellationToken cancellationToken)
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
                result.AddError("Структурное подразделение не найдено.");
                return result;
            }

            _context.Departments.Remove(existingDepartment);
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