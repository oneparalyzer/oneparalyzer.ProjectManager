using MediatR;
using Microsoft.AspNetCore.Mvc;
using oneparalyzer.ProjectManager.Application.Companies.Commands.Create;
using oneparalyzer.ProjectManager.Application.Companies.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Companies.Commands.Update;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Companies.Queries.GetByPage;

namespace oneparalyzer.ProjectManager.Api.Controllers;

[Route("companies")]
[ApiController]
public sealed class CompaniesController : ControllerBase
{
    private readonly ISender _mediator;

    public CompaniesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("create")]
    public async Task<IActionResult> CreateAsync(CreateCompanyCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpDelete("{id}/remove")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var result = await _mediator.Send(new RemoveCompanyByIdCommand(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetByPageAsync(GetCompaniesByPageQuery request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByPageAsync(Guid id)
    {
        var result = await _mediator.Send(new GetCompanyByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(UpdateCompanyCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
