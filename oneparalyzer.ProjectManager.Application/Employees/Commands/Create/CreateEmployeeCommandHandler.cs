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

namespace oneparalyzer.ProjectManager.Application.Employees.Commands.Create;

public sealed class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CreateEmployeeCommand> _validator;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;

    public CreateEmployeeCommandHandler(
        IApplicationDbContext context,
        IValidator<CreateEmployeeCommand> validator,
        ILogger<CreateEmployeeCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<SimpleResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
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
                return result;
            }

            var employee = new Employee(
                EmployeeId.Create(),
                new FullName(
                    request.Surname,
                    request.Name,
                    request.Patronymic),
                PostId.Create(request.PostId), 
                UserId.Create(request.UserId));

            await _context.Employees.AddAsync(employee, cancellationToken);
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