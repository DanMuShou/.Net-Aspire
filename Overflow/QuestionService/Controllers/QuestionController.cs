using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionService.Data;
using QuestionService.Dtos;
using QuestionService.Models;

namespace QuestionService.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionController(QuestionDbContext db) : ControllerBase
{
    // Models
    //  |- Question
    //      |- Dto
    //          |- QuestionCreateDTO
    //          |- QuestionPutDTO
    //          |- QuestionDelectDTO
    //      |- Question
    //      |- QuestionB

    // Controller
    // Resp
    //  |- QuestionResp
    //      |- QuestionResp
    //      |- IQuestionResp - CRUD
    // Serve
    //  |- QuestionServe
    //      |- QuestionServe
    //      |- IQuestionServe - IQuestionResp - CRUD

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Question>> CreateQuestion(
        [FromBody] QuestionCreateDto questionCreateDto
    )
    {
        var validTags = await db
            .Tags.Where(t => questionCreateDto.Tags.Contains(t.Slug))
            .ToListAsync();
        var missTags = questionCreateDto.Tags.Except(validTags.Select(t => t.Slug)).ToList();
        if (missTags.Count != 0)
            return BadRequest($"未定义标签：{string.Join(", ", missTags)}");

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name = User.FindFirstValue("name");

        if (userId is null || name is null)
            return BadRequest("无法获取用户详细信息");

        var question = new Question
        {
            Title = questionCreateDto.Title,
            Content = questionCreateDto.Content,
            AskerId = userId,
            AskerDisplayName = name,
            TagSlugs = questionCreateDto.Tags,
        };

        db.Questions.Add(question);
        await db.SaveChangesAsync();

        return Created($"/question/{question.Id}", question);
    }

    [HttpGet]
    public async Task<ActionResult<List<Question>>> GetQuestions([FromQuery] string? tag)
    {
        var query = db.Questions.AsQueryable();
        if (!string.IsNullOrWhiteSpace(tag))
            query = query.Where(q => q.TagSlugs.Contains(tag));
        query = query.OrderByDescending(q => q.CreateAt);
        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Question>> GetQuestion(string id)
    {
        var question = await db.Questions.FindAsync(id);
        if (question is null)
            return NotFound();
        await db
            .Questions.Where(q => q.Id == id)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(q => q.ViewCount, q => q.ViewCount + 1)
            );

        return question;
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Question>> UpdateQuestion(
        string id,
        [FromBody] QuestionCreateDto questionPutDto
    )
    {
        var question = await db.Questions.FindAsync(id);
        if (question is null)
            return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (question.AskerId != userId)
            return Forbid();

        var validTags = await db
            .Tags.Where(t => questionPutDto.Tags.Contains(t.Slug))
            .ToListAsync();
        var missTags = questionPutDto.Tags.Except(validTags.Select(t => t.Slug)).ToList();
        if (missTags.Count != 0)
            return BadRequest($"未定义标签：{string.Join(", ", missTags)}");

        question.Title = questionPutDto.Title;
        question.Content = questionPutDto.Content;
        question.TagSlugs = questionPutDto.Tags;
        question.UpdateAt = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return question;
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuestion(string id)
    {
        var question = await db.Questions.FindAsync(id);
        if (question is null)
            return NotFound();
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (question.AskerId != userId)
            return Forbid();
        db.Questions.Remove(question);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
