using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using oneparalyzer.ProjectManager.Application.Common.Interfaces;
using oneparalyzer.ProjectManager.Domain.Common.OperationResults;

namespace oneparalyzer.ProjectManager.Application.Companies.Commands.Remove;

public sealed class RemoveCompanyByIdCommandHandler : IRequestHandler<RemoveCompanyByIdCommand, SimpleResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<RemoveCompanyByIdCommand> _validator;
    private readonly ILogger<RemoveCompanyByIdCommandHandler> _logger;

    public RemoveCompanyByIdCommandHandler(
        IApplicationDbContext context,
        IValidator<RemoveCompanyByIdCommand> validator,
        ILogger<RemoveCompanyByIdCommandHandler> logger)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
    }

    public Task<SimpleResult> Handle(RemoveCompanyByIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
