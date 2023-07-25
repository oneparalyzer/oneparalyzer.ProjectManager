using MediatR;
using Microsoft.AspNetCore.Mvc;
using oneparalyzer.ProjectManager.Application.Departments.Commands.Create;
using oneparalyzer.ProjectManager.Application.Departments.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Departments.Commands.Update;
using oneparalyzer.ProjectManager.Application.Departments.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Departments.Queries.GetByPage;

namespace oneparalyzer.ProjectManager.Api.Controllers;

[Route("departments")]
[ApiController]
public sealed class DepartmentsController : ControllerBase
{
    private readonly ISender _mediator;

    public DepartmentsController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut("create")]
    public async Task<IActionResult> CreateAsync(CreateDepartmentCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpDelete("{id}/remove")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var result = await _mediator.Send(new RemoveDepartmentByIdCommand(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetByPageAsync(GetDepartmentsByPageQuery request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByPageAsync(Guid id)
    {
        var result = await _mediator.Send(new GetDepartmentByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(UpdateDepartmentCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}