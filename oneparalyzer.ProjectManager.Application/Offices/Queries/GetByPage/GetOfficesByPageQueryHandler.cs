using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Offices.Queries.GetByPage;

public sealed class GetOfficesByPageQueryHandler : IRequestHandler<GetOfficesByPageQuery, Result<IEnumerable<GetOfficesByPageModel>>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetOfficesByPageQuery> _validator;
    private readonly ILogger<GetOfficesByPageQueryHandler> _logger;

    public GetOfficesByPageQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetOfficesByPageQuery> validator,
        ILogger<GetOfficesByPageQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<IEnumerable<GetOfficesByPageModel>>> Handle(GetOfficesByPageQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<IEnumerable<GetOfficesByPageModel>>();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }
            
            IEnumerable<Office> offices = await _context.Offices
                .Where(x => 
                    x.CompanyId == CompanyId.Create(request.CompanyId))
                .OrderBy(x => x.Title)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            IEnumerable<GetOfficesByPageModel> officesModel = _mapper.Map<IEnumerable<GetOfficesByPageModel>>(offices);

            result.Data = officesModel;

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