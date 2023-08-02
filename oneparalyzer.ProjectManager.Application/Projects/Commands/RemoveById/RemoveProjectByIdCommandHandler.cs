using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Projects.Commands.RemoveById;

public sealed class RemoveProjectByIdCommandHandler : IRequestHandler<RemoveProjectByIdCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RemoveProjectByIdCommand> _validator;
    private readonly ILogger<RemoveProjectByIdCommandHandler> _logger;

    public RemoveProjectByIdCommandHandler(
        IApplicationDbContext context,
        IValidator<RemoveProjectByIdCommand> validator,
        ILogger<RemoveProjectByIdCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<SimpleResult> Handle(RemoveProjectByIdCommand request, CancellationToken cancellationToken)
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
                    x.Id == ProjectId.Create(request.Id),
                    cancellationToken);
            if (existingProject is null)
            {
                result.AddError("Проект не найден.");
                return result;
            }

            _context.Projects.Remove(existingProject);
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
