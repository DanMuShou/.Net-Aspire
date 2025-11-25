using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Dtos.Post;
using PostService.Models.Post;
using PostService.Services.Post;

namespace PostService.Controllers.Post;

[ApiController]
[Route("[controller]")]
public class PostAnswerController(IPostAnswerService answerService) : Controller
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<PostAnswerDto>> CreateAnswer(
        [FromBody] PostAnswerCreateDto answerCreateDto
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name = User.FindFirstValue("name");

        if (userId is null || name is null)
            return BadRequest("无法获取用户详细信息");

        var result = await answerService.CreateAnswerAsync(userId, name, answerCreateDto);
        return Created($"/answer/{result.Id}", result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostAnswerDto>> GetAnswer(string id)
    {
        var answer = await answerService.GetAnswerByIdAsync(id);
        if (answer is null)
            return NotFound();
        return answer;
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<PostAnswerDto>> UpdateAnswer(
        string id,
        [FromBody] PostAnswerUpdateDto answerUpdateDto
    )
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return BadRequest("无法获取用户详细信息");

        var answer = await answerService.UpdateAnswerAsync(id, userId, answerUpdateDto);

        if (answer is null)
            return NotFound("未找到指定的回答或无权访问");

        return answer;
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAnswer(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return BadRequest("无法获取用户详细信息");

        var result = await answerService.DeleteAnswerAsync(id, userId);
        if (!result)
            return NotFound("未找到指定的回答或无权访问");

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<PostAnswerDto>>> GetAnswers([FromQuery] string questionId)
    {
        var answers = await answerService.GetAnswersByQuestionIdAsync(questionId);
        return answers;
    }
}