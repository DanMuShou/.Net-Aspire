using System.Security.Claims;
using Contracts.MessageQueue.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Dtos;
using PostService.Dtos.Post;
using PostService.Models.Post;
using PostService.Services.Post;

namespace PostService.Controllers.Post;

[ApiController]
[Route("[controller]")]
public class PostQuestionController(IPostQuestionService questionService) : Controller
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<PostQuestion>> CreateQuestion(
        [FromBody] PostQuestionCreateDto questionCreateDto
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name = User.FindFirstValue("name");

        if (userId is null || name is null)
            return BadRequest("无法获取用户详细信息");
        var result = await questionService.CreateQuestionAsync(userId, name, questionCreateDto);
        return Created($"/question/{result.Id}", result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostQuestionDto>> GetQuestion(string id)
    {
        var question = await questionService.GetQuestionByIdAsync(id);
        if (question is null)
            return NotFound();
        return question;
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<PostQuestionDto>> UpdateQuestion(
        string id,
        [FromBody] PostQuestionUpdateDto questionPutDto
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return BadRequest("无法获取用户详细信息");
        var question = await questionService.UpdateQuestionAsync(id, userId, questionPutDto);
        if (question is null)
            return NotFound("未找到指定的问题或无权访问");
        return question;
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuestion(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return BadRequest("无法获取用户详细信息");
        var result = await questionService.DeleteQuestionAsync(id, userId);
        if (!result)
            return NotFound("未找到指定的问题或无权访问");
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<PostQuestionDto>>> GetQuestions([FromQuery] string? tag)
    {
        var questions = await questionService.GetQuestionsAsync(tag);
        return questions;
    }
}
