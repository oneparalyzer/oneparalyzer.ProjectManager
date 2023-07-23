using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Departments.Queries.GetByPage;

public sealed class GetDepartmentsByPageQueryHandler : IRequestHandler<GetDepartmentsByPageQuery, Result<IEnumerable<GetDepartmentsByPageModel>>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetDepartmentsByPageQuery> _validator;
    private readonly ILogger<GetDepartmentsByPageQueryHandler> _logger;

    public GetDepartmentsByPageQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetDepartmentsByPageQuery> validator,
        ILogger<GetDepartmentsByPageQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<IEnumerable<GetDepartmentsByPageModel>>> Handle(GetDepartmentsByPageQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<IEnumerable<GetDepartmentsByPageModel>>();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }
            
            IEnumerable<Department> departments = await _context.Departments
                .Where(x => 
                    x.OfficeId == OfficeId.Create(request.OfficeId))
                .OrderBy(x => x.Title)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            IEnumerable<GetDepartmentsByPageModel> departmentsModel = _mapper.Map<IEnumerable<GetDepartmentsByPageModel>>(departments);

            result.Data = departmentsModel;

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