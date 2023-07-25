using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;
using oneparalyzer.ProjectManager.Domain.Common.ValueObjects;

namespace oneparalyzer.ProjectManager.Application.Employees.Commands.Update;

public sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<UpdateEmployeeCommand> _validator;
    private readonly ILogger<UpdateEmployeeCommandHandler> _logger;

    public UpdateEmployeeCommandHandler(
        IApplicationDbContext context,
        IValidator<UpdateEmployeeCommand> validator,
        ILogger<UpdateEmployeeCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<SimpleResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
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

            Post? existinfPost = await _context.Posts
                .FirstOrDefaultAsync(x =>
                        x.Id == PostId.Create(request.PostId),
                    cancellationToken);
            if (existinfPost is null)
            {
                result.AddError("Должность не найдена.");
            }

            Employee? existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(x =>
                        x.Id == EmployeeId.Create(request.Id),
                    cancellationToken);
            if (existingEmployee is null)
            {
                result.AddError("Работник не найдена.");
            }

            if (!result.Succeed)
            {
                return result;
            }
            
            existingEmployee.Update(
                new FullName(
                    request.Surname,
                    request.Name,
                    request.Patronymic),
                PostId.Create(request.PostId), 
                UserId.Create(request.UserId));

            _context.Employees.Update(existingEmployee);
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