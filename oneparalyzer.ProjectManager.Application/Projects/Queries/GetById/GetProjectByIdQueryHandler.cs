using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Projects.Queries.GetById;

public sealed class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Result<GetProjectByIdModel>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetProjectByIdQuery> _validator;
    private readonly ILogger<GetProjectByIdQueryHandler> _logger;

    public GetProjectByIdQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetProjectByIdQuery> validator,
        ILogger<GetProjectByIdQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<GetProjectByIdModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<GetProjectByIdModel>();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }

            Project? existingProject = await _context.Projects.FirstOrDefaultAsync(x =>
                    x.Id == ProjectId.Create(request.Id),
                cancellationToken);
            if (existingProject is null)
            {
                result.AddError("Проект не найден.");
                return result;
            }

            GetProjectByIdModel projectModel = _mapper.Map<GetProjectByIdModel>(existingProject);

            result.Data = projectModel;

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
