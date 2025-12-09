using Application.Contracts.Repositories.PostServer;
using Domain.Entity.PostServer.Post;

namespace Persistence.Repositories.PostServer;

public class PostAnswerRepository(PostServerDbContext context)
    : Repository<PostAnswer>(context),
        IPostAnswerRepository { }
