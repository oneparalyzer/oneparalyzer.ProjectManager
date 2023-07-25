using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Employees.Commands.RemoveById;

public class RemoveEmployeeByIdCommandHandler : IRequestHandler<RemoveEmployeeByIdCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RemoveEmployeeByIdCommand> _validator;
    private readonly ILogger<RemoveEmployeeByIdCommandHandler> _logger;

    public RemoveEmployeeByIdCommandHandler(
        IApplicationDbContext context,
        IValidator<RemoveEmployeeByIdCommand> validator,
        ILogger<RemoveEmployeeByIdCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<SimpleResult> Handle(RemoveEmployeeByIdCommand request, CancellationToken cancellationToken)
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

            Employee? existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(x =>
                    x.Id == EmployeeId.Create(request.Id),
                    cancellationToken);
            if (existingEmployee is null)
            {
                result.AddError("Сотрудник не найден.");
                return result;
            }

            _context.Employees.Remove(existingEmployee);
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