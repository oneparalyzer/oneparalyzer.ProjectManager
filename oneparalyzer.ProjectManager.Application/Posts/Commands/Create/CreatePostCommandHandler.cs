using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Posts.Commands.Create;

public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CreatePostCommand> _validator;
    private readonly ILogger<CreatePostCommandHandler> _logger;

    public CreatePostCommandHandler(
        IApplicationDbContext context,
        IValidator<CreatePostCommand> validator,
        ILogger<CreatePostCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<SimpleResult> Handle(CreatePostCommand request, CancellationToken cancellationToken)
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

            Department? existingDepartment = await _context.Departments
                .FirstOrDefaultAsync(x =>
                    x.Id == DepartmentId.Create(request.DepartmentId),
                    cancellationToken);
            if (existingDepartment is null)
            {
                result.AddError("Структурное подразделение не найдено.");
                return result;
            }

            var post = new Post(
                PostId.Create(),
                request.Title,
                DepartmentId.Create(request.DepartmentId));

            await _context.Posts.AddAsync(post, cancellationToken);
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