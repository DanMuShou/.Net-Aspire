using Microsoft.EntityFrameworkCore;
using PostService.Data;
using PostService.Models.Post;

namespace PostService.Repositories.Post;

public class PostQuestionRepository(PostDbContext db) : IPostQuestionRepository
{
    private DbSet<PostQuestion> Set => db.Set<PostQuestion>();

    public async Task<PostQuestion> CreateQuestionAsync(PostQuestion postQuestion)
    {
        await Set.AddAsync(postQuestion);
        await db.SaveChangesAsync();
        return postQuestion;
    }

    public async Task<PostQuestion?> GetQuestionByIdAsync(string id)
    {
        var rowsAffected = await Set.Where(q => q.Id == id)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(q => q.ViewCount, q => q.ViewCount + 1)
            );
        if (rowsAffected > 0)
            return await Set.FindAsync(id);
        return null;
    }

    public async Task<PostQuestion?> UpdateQuestionAsync(PostQuestion postQuestion, string userId)
    {
        var match = await Set.FirstOrDefaultAsync(q =>
            q.Id == postQuestion.Id && q.AskerId == userId
        );
        if (match is null)
            return null;

        match.Title = postQuestion.Title;
        match.Content = postQuestion.Content;
        match.TagSlugs = postQuestion.TagSlugs;
        match.UpdateAt = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return match;
    }

    public async Task<bool> DeleteQuestionAsync(string id, string userId)
    {
        var match = await Set.FirstOrDefaultAsync(q => q.Id == id && q.AskerId == userId);
        if (match is null)
            return false;
        Set.Remove(match);
        return await db.SaveChangesAsync() > 0;
    }

    public async Task<List<PostQuestion>> GetQuestionsAsync(string? tag)
    {
        if (tag is not null)
            return await Set.AsNoTracking().Where(q => q.TagSlugs.Contains(tag)).ToListAsync();
        return await Set.AsNoTracking().ToListAsync();
    }
}
