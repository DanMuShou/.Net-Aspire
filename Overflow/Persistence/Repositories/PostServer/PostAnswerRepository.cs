using Application.Contracts.Repositories.PostServer;
using Domain.Entity.PostServer.Post;

namespace Persistence.Repositories.PostServer;

public class PostAnswerRepository(PostServerDbContext context)
    : Repository<PostAnswer>(context),
        IPostAnswerRepository
{
    private readonly PostServerDbContext _context = context;

    public Task DeleteFromQuestionIdAsync(Guid questionId)
    {
        var answers = _context.Set<PostAnswer>().Where(x => x.PostQuestionId == questionId);
        _context.Set<PostAnswer>().RemoveRange(answers);
        return Task.CompletedTask;
    }

    public Task<List<PostAnswer>> GetByQuestionIdAsync(Guid questionId)
    {
        return Task.FromResult(
            _context.Set<PostAnswer>().Where(x => x.PostQuestionId == questionId).ToList()
        );
    }
}
