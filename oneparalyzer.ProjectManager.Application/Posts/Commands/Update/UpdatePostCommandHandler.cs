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

namespace oneparalyzer.ProjectManager.Application.Posts.Commands.Update;

public sealed class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<UpdatePostCommand> _validator;
    private readonly ILogger<UpdatePostCommandHandler> _logger;

    public UpdatePostCommandHandler(
        IApplicationDbContext context,
        IValidator<UpdatePostCommand> validator,
        ILogger<UpdatePostCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<SimpleResult> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
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
                    x.Id == DepartmentId.Create(request.NewDepartmentId),
                    cancellationToken);
            if (existingDepartment is null)
            {
                result.AddError("Структурное подразделение не найдено.");
            }
            
            Post? existingPost = await _context.Posts
                .FirstOrDefaultAsync(x =>
                        x.Id == PostId.Create(request.Id),
                    cancellationToken);
            if (existingPost is null)
            {
                result.AddError("Должность не найдена.");
            }

            if (!result.Succeed)
            {
                return result;
            }
            
            existingPost.Update(request.NewTitle, DepartmentId.Create(request.NewDepartmentId));
            _context.Posts.Update(existingPost);
            await _context.SaveChangesAsync(cancellationToken);
            
            return result;
        }
        catch (Exception ex)
        {
            result.AddError("Произошда ошибка.");
            _logger.LogError(ex.Message);
            return result;
        }
    }
}