using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.Entities;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Projects.Commands.Create;

public sealed class CreateProjectCommandHanler : IRequestHandler<CreateProjectCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateProjectCommandHanler> _logger;
    private readonly IValidator<CreateProjectCommand> _validator;

    public CreateProjectCommandHanler(
        IApplicationDbContext context,
        ILogger<CreateProjectCommandHanler> logger,
        IValidator<CreateProjectCommand> validator)
    {
        _context = context;
        _logger = logger;
        _validator = validator;
    }

    public async Task<SimpleResult> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var result = new SimpleResult();

        try
        {
            var projectTasks = new List<ProjectTask>();

            foreach (var requestProjectTask in request.ProjectTasks)
            {
                projectTasks.Add(new ProjectTask(
                    ProjectTaskId.Create(),
                    requestProjectTask.Number,
                    requestProjectTask.Description,
                    requestProjectTask.IsCompleted,
                    EmployeeId.Create(requestProjectTask.ResponsibleEmployeeId)));
            }

            var project = new Project(
                ProjectId.Create(),
                request.Title,
                EmployeeId.Create(request.EmployeeId),
                projectTasks);

            await _context.Projects.AddAsync(project, cancellationToken);
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
