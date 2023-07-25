using MediatR;
using Microsoft.AspNetCore.Mvc;
using oneparalyzer.ProjectManager.Application.Posts.Commands.Create;
using oneparalyzer.ProjectManager.Application.Posts.Commands.RemoveById;
using oneparalyzer.ProjectManager.Application.Posts.Commands.Update;
using oneparalyzer.ProjectManager.Application.Posts.Queries.GetById;
using oneparalyzer.ProjectManager.Application.Posts.Queries.GetByPage;


namespace oneparalyzer.ProjectManager.Api.Controllers;

[Route("posts")]
[ApiController]
public sealed class PostsController : ControllerBase
{
    private readonly ISender _mediator;

    public PostsController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut("create")]
    public async Task<IActionResult> CreateAsync(CreatePostCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpDelete("{id}/remove")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var result = await _mediator.Send(new RemovePostByIdCommand(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetByPageAsync(GetPostsByPageQuery request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByPageAsync(Guid id)
    {
        var result = await _mediator.Send(new GetPostByIdQuery(id));
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(UpdatePostCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}