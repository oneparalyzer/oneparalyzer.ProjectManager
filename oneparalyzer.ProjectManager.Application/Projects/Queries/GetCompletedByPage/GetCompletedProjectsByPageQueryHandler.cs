using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.AggregateModels.DepartmentAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.EmployeeAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.PostAggregate.ValueObjects;
using oneparalyzer.ProjectManager.Domain.AggregateModels.ProjectAggregate;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Projects.Queries.GetCompletedByPage;

public sealed class GetCompletedProjectsByPageQueryHandler : IRequestHandler<GetCompletedProjectsByPageQuery, Result<IEnumerable<GetCompletedProjectsByPageModel>>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetCompletedProjectsByPageQuery> _validator;
    private readonly ILogger<GetCompletedProjectsByPageQueryHandler> _logger;

    public GetCompletedProjectsByPageQueryHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IValidator<GetCompletedProjectsByPageQuery> validator,
        ILogger<GetCompletedProjectsByPageQueryHandler> logger)
    {
        _mapper = mapper;
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<GetCompletedProjectsByPageModel>>> Handle(GetCompletedProjectsByPageQuery request, CancellationToken cancellationToken)
    {
        var result = new Result<IEnumerable<GetCompletedProjectsByPageModel>>();

        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                result.AddValidationErrors(validationResult.ToDictionary());
                return result;
            }

            IEnumerable<PostId> postIds = await _context.Posts
                .Where(x =>
                    x.DepartmentId == DepartmentId.Create(request.DepartmentId))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            IEnumerable<EmployeeId> employeeIds = await _context.Employees
                .Where(x =>
                    postIds.Contains(x.PostId))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            IEnumerable<Project> existingProjects = await _context.Projects
                .Where(x =>
                    employeeIds.Contains(x.EmployeeId) &&
                    x.IsCompleted())
                .OrderBy(x => x.Title)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            IEnumerable<GetCompletedProjectsByPageModel> projectsModel =
                _mapper.Map<IEnumerable<GetCompletedProjectsByPageModel>>(existingProjects);

            result.Data = projectsModel;

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