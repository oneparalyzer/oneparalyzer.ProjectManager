using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.RemoveById;

public sealed class RemoveProjectTaskByIdCommandHandler : IRequestHandler<RemoveProjectTaskByIdCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RemoveProjectTaskByIdCommand> _validator;
    private readonly ILogger<RemoveProjectTaskByIdCommandHandler> _logger;

    public RemoveProjectTaskByIdCommandHandler(
        IApplicationDbContext context,
        IValidator<RemoveProjectTaskByIdCommand> validator,
        ILogger<RemoveProjectTaskByIdCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<SimpleResult> Handle(RemoveProjectTaskByIdCommand request, CancellationToken cancellationToken)
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


            SimpleResult removeProjectTaskResult =
                existingProject.RemoveProjectTaskById(ProjectTaskId.Create(request.ProjectTaskId));
            if (removeProjectTaskResult.Succeed)
            {
                result.AddErrors(removeProjectTaskResult.Errors.ToList());
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

