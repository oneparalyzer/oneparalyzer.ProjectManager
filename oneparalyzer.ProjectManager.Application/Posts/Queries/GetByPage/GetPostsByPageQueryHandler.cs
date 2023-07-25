using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Application.Offices.Queries.GetByPage;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Posts.Queries.GetByPage;

public sealed class GetPostsByPageQueryHandler : IRequestHandler<GetPostsByPageQuery, Result<IEnumerable<GetPostsByPageModel>>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetPostsByPageQuery> _validator;
    private readonly ILogger<GetPostsByPageQueryHandler> _logger;

    public GetPostsByPageQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetPostsByPageQuery> validator,
        ILogger<GetPostsByPageQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<GetPostsByPageModel>>> Handle(GetPostsByPageQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<IEnumerable<GetPostsByPageModel>>();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }

            IEnumerable<Post> posts = await _context.Posts
                .Where(x =>
                    x.DepartmentId == DepartmentId.Create(request.DepartmentId))
                .OrderBy(x => x.Title)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            IEnumerable<GetPostsByPageModel> postsModel = _mapper.Map<IEnumerable<GetPostsByPageModel>>(posts);

            result.Data = postsModel;

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
