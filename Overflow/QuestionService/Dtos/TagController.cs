using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionService.Data;
using QuestionService.Models;

namespace QuestionService.Dtos;

[ApiController]
[Route("[controller]")]
public class TagController(QuestionDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Tag>>> GetTags()
    {
        return await db.Tags.OrderBy(t => t.Name).ToListAsync();
    }
}
