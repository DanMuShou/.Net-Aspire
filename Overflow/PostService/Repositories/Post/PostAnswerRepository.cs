using Microsoft.EntityFrameworkCore;
using PostService.Data;
using PostService.Models.Post;

namespace PostService.Repositories.Post;

public class PostAnswerRepository(PostDbContext db) : IPostAnswerRepository
{
    private DbSet<PostAnswer> Set => db.Set<PostAnswer>();

    public async Task<PostAnswer> CreateAnswerAsync(PostAnswer postAnswer)
    {
        await Set.AddAsync(postAnswer);
        await db.SaveChangesAsync();
        return postAnswer;
    }

    public async Task<PostAnswer?> GetAnswerByIdAsync(string id)
    {
        return await Set.FindAsync(id);
    }

    public async Task<PostAnswer?> UpdateAnswerAsync(PostAnswer postAnswer, string userId)
    {
        var match = await Set.FirstOrDefaultAsync(a => 
            a.Id == postAnswer.Id && a.UserId == userId
        );
        if (match is null)
            return null;

        match.Content = postAnswer.Content;
        match.IsAccepted = postAnswer.IsAccepted;
        await db.SaveChangesAsync();
        return match;
    }

    public async Task<bool> DeleteAnswerAsync(string id, string userId)
    {
        var match = await Set.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (match is null)
            return false;
        Set.Remove(match);
        return await db.SaveChangesAsync() > 0;
    }

    public async Task<List<PostAnswer>> GetAnswersByQuestionIdAsync(string questionId)
    {
        return await Set.AsNoTracking()
            .Where(a => a.PostQuestionId == questionId)
            .ToListAsync();
    }
}