using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Departments.Queries.GetById;

public sealed class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Result<GetDepartmentByIdModel>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetDepartmentByIdQuery> _validator;
    private readonly ILogger<GetDepartmentByIdQueryHandler> _logger;

    public GetDepartmentByIdQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetDepartmentByIdQuery> validator,
        ILogger<GetDepartmentByIdQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<GetDepartmentByIdModel>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<GetDepartmentByIdModel>();

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
                        x.Id == DepartmentId.Create(request.Id),
                    cancellationToken);
            if (existingDepartment is null)
            {
                result.AddError("Структурное подразделение не найдено");
                return result;
            }

            GetDepartmentByIdModel departmentModel = _mapper.Map<GetDepartmentByIdModel>(existingDepartment);

            result.Data = departmentModel;

            return result;
        }
        catch (Exception ex)
        {
            result.AddError("Произошла ошибкаю.");
            _logger.LogError(ex.Message);
            return result;
        }
    }
}