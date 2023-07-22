using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;

public sealed class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Result<GetCompanyByIdModel>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetCompanyByIdQuery> _validator;
    private readonly ILogger<GetCompanyByIdQueryHandler> _logger;

    public GetCompanyByIdQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetCompanyByIdQuery> validator,
        ILogger<GetCompanyByIdQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<GetCompanyByIdModel>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<GetCompanyByIdModel>();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }

            Company? company = await _context.Companies
                .FirstOrDefaultAsync(x =>
                    x.Id == CompanyId.Create(request.Id),
                    cancellationToken);
            if (company is null)
            {
                result.AddError("Компания не найдена.");
                return result;
            }

            GetCompanyByIdModel companyModel = _mapper.Map<GetCompanyByIdModel>(company);
            result.Data = companyModel;
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
