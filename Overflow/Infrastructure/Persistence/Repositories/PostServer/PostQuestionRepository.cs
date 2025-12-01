using Application.Contracts.Repositories.PostServer;
using Domain.Entity.PostServer.Post;

namespace Persistence.Repositories.PostServer;

public class PostQuestionRepository(PostServerDbContext context)
    : Repository<PostQuestion>(context),
        IPostQuestionRepository { }
