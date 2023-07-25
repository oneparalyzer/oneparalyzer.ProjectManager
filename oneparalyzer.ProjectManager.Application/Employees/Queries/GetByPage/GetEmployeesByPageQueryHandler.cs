using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Employees.Queries.GetByPage;

public sealed class GetEmployeesByPageQueryHandler : IRequestHandler<GetEmployeesByPageQuery, Result<IEnumerable<GetEmployeesByPageModel>>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetEmployeesByPageQuery> _validator;
    private readonly ILogger<GetEmployeesByPageQueryHandler> _logger;

    public GetEmployeesByPageQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetEmployeesByPageQuery> validator,
        ILogger<GetEmployeesByPageQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<IEnumerable<GetEmployeesByPageModel>>> Handle(GetEmployeesByPageQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<IEnumerable<GetEmployeesByPageModel>>();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }
            
            IEnumerable<Employee> employees = await _context.Employees
                .Where(x => 
                    x.PostId == PostId.Create(request.PostId))
                .OrderBy(x => x.FullName.Surname)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            IEnumerable<GetEmployeesByPageModel> employeesModel = _mapper.Map<IEnumerable<GetEmployeesByPageModel>>(employees);

            result.Data = employeesModel;

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