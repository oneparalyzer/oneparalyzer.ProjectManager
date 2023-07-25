using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Posts.Commands.RemoveById;

public sealed class RemovePostByIdCommandHandler : IRequestHandler<RemovePostByIdCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RemovePostByIdCommand> _validator;
    private readonly ILogger<RemovePostByIdCommandHandler> _logger;

    public RemovePostByIdCommandHandler(
        IApplicationDbContext context,
        IValidator<RemovePostByIdCommand> validator,
        ILogger<RemovePostByIdCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<SimpleResult> Handle(RemovePostByIdCommand request, CancellationToken cancellationToken)
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

            Post? existingPost = await _context.Posts
                .FirstOrDefaultAsync(x =>
                        x.Id == PostId.Create(request.Id),
                    cancellationToken);
            if (existingPost is null)
            {
                result.AddError("Должность не найдена.");
                return result;
            }

            _context.Posts.Remove(existingPost);
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