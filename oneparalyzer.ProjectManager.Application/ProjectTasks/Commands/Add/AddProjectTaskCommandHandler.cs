using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.Entities;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Add;

public sealed class AddProjectTaskCommandHandler : IRequestHandler<AddProjectTaskCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<AddProjectTaskCommand> _validator;
    private readonly ILogger<AddProjectTaskCommandHandler> _logger;

    public AddProjectTaskCommandHandler(
        IApplicationDbContext context,
        IValidator<AddProjectTaskCommand> validator,
        ILogger<AddProjectTaskCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<SimpleResult> Handle(AddProjectTaskCommand request, CancellationToken cancellationToken)
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

            var newProjectTask = new ProjectTask(
                ProjectTaskId.Create(),
                request.Number,
                request.Description,
                EmployeeId.Create(request.ResponsibleEmployeeId));

            SimpleResult addProjectTaskResult = 
                existingProject.AddProjectTask(newProjectTask);
            if (addProjectTaskResult.Succeed)
            {
                result.AddErrors(addProjectTaskResult.Errors.ToList());
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
