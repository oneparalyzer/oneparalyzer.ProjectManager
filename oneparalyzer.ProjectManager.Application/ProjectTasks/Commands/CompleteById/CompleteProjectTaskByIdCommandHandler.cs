using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.CompleteById;

public sealed class CompleteProjectTaskByIdCommandHandler : IRequestHandler<CompleteProjectTaskByIdCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CompleteProjectTaskByIdCommand> _validator;
    private readonly ILogger<CompleteProjectTaskByIdCommandHandler> _logger;

    public CompleteProjectTaskByIdCommandHandler(
        IApplicationDbContext context,
        IValidator<CompleteProjectTaskByIdCommand> validator,
        ILogger<CompleteProjectTaskByIdCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<SimpleResult> Handle(CompleteProjectTaskByIdCommand request, CancellationToken cancellationToken)
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

            Project? existingProject = await _context.Projects
                .FirstOrDefaultAsync(x =>
                        x.Id == ProjectId.Create(request.ProjectId),
                    cancellationToken);
            if (existingProject is null)
            {
                result.AddError("Проект не найден.");
                return result;
            }

            SimpleResult completeProjectTaskResult = 
                existingProject.CompleteProjectTaskById(ProjectTaskId.Create(request.ProjectTaskId));
            if (!completeProjectTaskResult.Succeed)
            {
                result.AddErrors(completeProjectTaskResult.Errors.ToList());
                return result;
            }

            _context.Projects.Update(existingProject);
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