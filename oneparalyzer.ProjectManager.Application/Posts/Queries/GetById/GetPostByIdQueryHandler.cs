using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Posts.Queries.GetById;

public sealed class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Result<GetPostByIdModel>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetPostByIdQuery> _validator;
    private readonly ILogger<GetPostByIdQueryHandler> _logger;

    public GetPostByIdQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetPostByIdQuery> validator,
        ILogger<GetPostByIdQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<GetPostByIdModel>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<GetPostByIdModel>();

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

            GetPostByIdModel postModel = _mapper.Map<GetPostByIdModel>(existingPost);

            result.Data = postModel;

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
