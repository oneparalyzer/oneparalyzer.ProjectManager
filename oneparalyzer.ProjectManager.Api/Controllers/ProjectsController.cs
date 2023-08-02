using MediatR;
using Microsoft.AspNetCore.Mvc;
using oneparalyzer.ProjectManager.Application.Posts.Queries.GetByPage;
using oneparalyzer.ProjectManager.Application.Projects.Commands.Create;
using oneparalyzer.ProjectManager.Application.Projects.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Projects.Commands.Update;
using oneparalyzer.ProjectManager.Application.Projects.Queries.GetById;
using oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Add;
using oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.CompleteById;
using oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.ProjectTasks.Commands.Update;

namespace oneparalyzer.ProjectManager.Api.Controllers;

[Route("projects")]
[ApiController]
public sealed class ProjectsController : ControllerBase
{
    private readonly ISender _mediator;

    public ProjectsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("create")]
    public async Task<IActionResult> CreateAsync(CreateProjectCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpDelete("{id}/remove")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var result = await _mediator.Send(new RemoveProjectByIdCommand(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetByPageAsync(GetProjectsByPageQuery request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByPageAsync(Guid id)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(UpdateProjectCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("update-project-task")]
    public async Task<IActionResult> UpdateAsync(UpdateProjectTaskCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("add-project-task")]
    public async Task<IActionResult> AddProjectTaskAsync(AddProjectTaskCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("complete-project-task")]
    public async Task<IActionResult> CompleteProjectTaskAsync(CompleteProjectTaskByIdCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("remove-project-task")]
    public async Task<IActionResult> RemoveProjectTaskAsync(RemoveProjectTaskByIdCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
