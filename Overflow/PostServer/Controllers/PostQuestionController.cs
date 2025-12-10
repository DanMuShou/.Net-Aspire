using System.Security.Claims;
using Application.DTO.PostServer.PostQuestions;
using Application.MediatR.Commands.PostServer.PostQuestions;
using Application.MediatR.Queries.PostServer.PostQuestions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostServer.DTO.Post;

namespace PostServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostQuestionController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<PostQuestionDto>> CreatePostQuestion(
        [FromBody] CreatePostQuestionDto questionDto
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name = User.FindFirstValue("name");

        if (userId is null || name is null)
            return BadRequest("无法获取用户详细信息");

        var command = new PostQuestionCreateCommand(
            questionDto.Title,
            questionDto.Content,
            userId,
            questionDto.Tags
        );
        var result = await mediator.Send(command);

        return Created($"/question/{result}", result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PostQuestionDto>> GetPostQuestion(Guid id)
    {
        var query = new PostQuestionGetQuery(id);
        var result = await mediator.Send(query);
        return result;
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdatePostQuestion(
        Guid id,
        [FromBody] UpdatePostQuestionDto questionDto
    )
    {
        var command = new PostQuestionUpdateCommand(
            id,
            questionDto.Title,
            questionDto.Content,
            questionDto.Tags,
            questionDto.HasAcceptedAnswer
        );

        await mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeletePostQuestion(Guid id)
    {
        var command = new PostQuestionDeleteCommand(id);
        await mediator.Send(command);

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<PostQuestionDto>>> GetQuestions([FromQuery] string? tag)
    {
        var query = new PostQuestionGetListQuery(tag);
        var questions = await mediator.Send(query);
        return questions;
    }

    [Authorize]
    [HttpPost("{postQuestionId:guid}/answer")]
    public async Task<ActionResult<PostAnswerDto>> CreatePostAnswer(
        Guid postQuestionId,
        [FromBody] CreatePostAnswerDto answerDto
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name = User.FindFirstValue("name");
        if (userId is null || name is null)
            return BadRequest("无法获取用户详细信息");

        var command = new PostAnswerCreateCommand(answerDto.Content, userId, postQuestionId);
        var result = await mediator.Send(command);

        return Created($"/question/{postQuestionId}/answer/{result}", result);
    }

    [Authorize]
    [HttpPut("{questionId:guid}/answer/{answerId:guid}")]
    public async Task<ActionResult> UpdatePostAnswer(
        Guid questionId,
        Guid answerId,
        [FromBody] UpdatePostAnswerDto answerDto
    )
    {
        var command = new PostAnswerUpdateCommand(answerId, questionId, answerDto.Content);
        await mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{questionId:guid}/answer/{answerId:guid}")]
    public async Task<ActionResult> DeletePostAnswer(Guid questionId, Guid answerId)
    {
        var command = new PostAnswerDeleteCommand(answerId);
        await mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpGet("{questionId:guid}/answer/{answerId:guid}/accept")]
    public async Task<ActionResult> AcceptPostAnswer(Guid questionId, Guid answerId)
    {
        var command = new PostAnswerAcceptCommand(answerId, questionId);
        await mediator.Send(command);
        return NoContent();
    }
}
