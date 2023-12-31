﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.OfficeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Offices.Queries.GetById;

public sealed class GetOfficeByIdQueryHandler : IRequestHandler<GetOfficeByIdQuery, Result<GetOfficeByIdModel>>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetOfficeByIdQuery> _validator;
    private readonly ILogger<GetOfficeByIdQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetOfficeByIdQueryHandler(
        IApplicationDbContext context,
        IValidator<GetOfficeByIdQuery> validator,
        ILogger<GetOfficeByIdQueryHandler> logger,
        IMapper mapper)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<Result<GetOfficeByIdModel>> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<GetOfficeByIdModel>();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }

            Office? existingOffice = await _context.Offices
                .FirstOrDefaultAsync(x =>
                        x.Id == OfficeId.Create(request.Id),
                    cancellationToken);
            if (existingOffice is null)
            {
                result.AddError("Офис не найден.");
                return result;
            }

            GetOfficeByIdModel officeModel = _mapper.Map<GetOfficeByIdModel>(existingOffice);

            result.Data = officeModel;

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