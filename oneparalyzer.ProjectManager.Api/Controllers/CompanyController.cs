using MediatR;
using Microsoft.AspNetCore.Mvc;
using oneparalyzer.ProjectManager.Application.Companies.Commands.Create;

namespace oneparalyzer.ProjectManager.Api.Controllers;

[Route("companies")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ISender _mediator;

    public CompanyController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("create")]
    public async Task<IActionResult> CreateAsync(CreateCompanyCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
