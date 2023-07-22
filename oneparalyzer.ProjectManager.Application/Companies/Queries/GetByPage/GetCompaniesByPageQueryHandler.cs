using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;
using oneparalyzer.ProjectManager.Domain.AggregateModels.CompanyAggregate;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;
using System.Collections.Generic;

namespace oneparalyzer.ProjectManager.Application.Companies.Queries.GetByPage;

public sealed class GetCompaniesByPageQueryHandler : IRequestHandler<GetCompaniesByPageQuery, Result<IEnumerable<GetCompaniesByPageModel>>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetCompaniesByPageQuery> _validator;
    private readonly ILogger<GetCompaniesByPageQueryHandler> _logger;

    public GetCompaniesByPageQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetCompaniesByPageQuery> validator,
        ILogger<GetCompaniesByPageQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<GetCompaniesByPageModel>>> Handle(GetCompaniesByPageQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<IEnumerable<GetCompaniesByPageModel>>();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }

            IEnumerable<Company> companies = await _context.Companies
                .OrderBy(x => x.Title)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            IEnumerable<GetCompaniesByPageModel> companiesModel = _mapper
                .Map<IEnumerable<GetCompaniesByPageModel>>(companies);

            result.Data = companiesModel;

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
