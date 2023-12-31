﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Employees.Queries.GetById;

public sealed class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Result<GetEmployeeByIdModel>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetEmployeeByIdQuery> _validator;
    private readonly ILogger<GetEmployeeByIdQueryHandler> _logger;

    public GetEmployeeByIdQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetEmployeeByIdQuery> validator,
        ILogger<GetEmployeeByIdQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<GetEmployeeByIdModel>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<GetEmployeeByIdModel>();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }

            Employee? existingEmployee = await _context.Employees
                .FirstOrDefaultAsync(x =>
                        x.Id == EmployeeId.Create(request.Id),
                    cancellationToken);
            if (existingEmployee is null)
            {
                result.AddError("Сотрудник не найден.");
                return result;
            }

            GetEmployeeByIdModel employeeModel = _mapper.Map<GetEmployeeByIdModel>(existingEmployee);

            result.Data = employeeModel;
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