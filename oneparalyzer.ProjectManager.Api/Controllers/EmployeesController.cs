using MediatR;
using Microsoft.AspNetCore.Mvc;
using oneparalyzer.ProjectManager.Application.Employees.Commands.Create;
using oneparalyzer.ProjectManager.Application.Employees.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Employees.Commands.Update;
using oneparalyzer.ProjectManager.Application.Employees.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Employees.Queries.GetByPage;

namespace oneparalyzer.ProjectManager.Api.Controllers;

[Route("employees")]
[ApiController]
public class EmployeesController : Controller
{
    private readonly ISender _mediator;

    public EmployeesController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut("create")]
    public async Task<IActionResult> CreateAsync(CreateEmployeeCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpDelete("{id}/remove")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var result = await _mediator.Send(new RemoveEmployeeByIdCommand(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetByPageAsync(GetEmployeesByPageQuery request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByPageAsync(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(UpdateEmployeeCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}