using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Add;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.Entities;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Update;

public sealed class UpdateProjectTaskCommandHandler : IRequestHandler<UpdateProjectTaskCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<UpdateProjectTaskCommand> _validator;
    private readonly ILogger<UpdateProjectTaskCommandHandler> _logger;

    public UpdateProjectTaskCommandHandler(
        IApplicationDbContext context,
        IValidator<UpdateProjectTaskCommand> validator,
        ILogger<UpdateProjectTaskCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<SimpleResult> Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
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
                request.NewNumber,
                request.NewDescription,
                EmployeeId.Create(request.NewResponsibleEmployeeId));

            SimpleResult updateProjectTaskResult =
                existingProject.UpdateProjectTask(newProjectTask);
            if (updateProjectTaskResult.Succeed)
            {
                result.AddErrors(updateProjectTaskResult.Errors.ToList());
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
