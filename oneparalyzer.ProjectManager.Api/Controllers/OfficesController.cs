using MediatR;
using Microsoft.AspNetCore.Mvc;
using oneparalyzer.ProjectManager.Application.Offices.Commands.Create;
using oneparalyzer.ProjectManager.Application.Offices.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Offices.Commands.Update;
using oneparalyzer.ProjectManager.Application.Offices.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Offices.Queries.GetByPage;

namespace oneparalyzer.ProjectManager.Api.Controllers;

public class OfficesController : ControllerBase
{
    private readonly ISender _mediator;

    public OfficesController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut("create")]
    public async Task<IActionResult> CreateAsync(CreateOfficeCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpDelete("{id}/remove")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var result = await _mediator.Send(new RemoveOfficeByIdCommand(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetByPageAsync(GetOfficesByPageQuery request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByPageAsync(Guid id)
    {
        var result = await _mediator.Send(new GetOfficeByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(UpdateOfficeCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}