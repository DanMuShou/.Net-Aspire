using System.Security.Claims;
using Application.DTO.PostServer.PostQuestions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using Application.MediatR.Queries.PostServer.PostQuestions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostServer.API.DTO.Post;

namespace PostServer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PostQuestionDto>> GetPostQuestion(Guid id)
    {
        var query = new GetPostQuestionQuery(id);
        var result = await mediator.Send(query);
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<PostQuestionDto>> CreatePostQuestion(
        [FromBody] CreatePostQuestionDto questionDto
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name = User.FindFirstValue("name");

        if (userId is null || name is null)
            return BadRequest("无法获取用户详细信息");

        var command = new CreatePostQuestionCommand(
            questionDto.Title,
            questionDto.Content,
            userId,
            questionDto.Tags
        );
        var result = await mediator.Send(command);

        return Created($"/question/{result}", result);
    }

    [HttpGet]
    public async Task<ActionResult<List<PostQuestionDto>>> GetQuestions([FromQuery] string? tag)
    {
        var query = new GetPostQuestionListQuery(tag);
        var questions = await mediator.Send(query);
        return questions;
    }
}
