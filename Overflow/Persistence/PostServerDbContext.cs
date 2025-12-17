using Domain.Entity.PostServer.Post;
using Microsoft.EntityFrameworkCore;
using Persistence.Configs;

namespace Persistence;

public class PostServerDbContext : DbContext
{
    public DbSet<PostQuestion> PostQuestions { get; set; }
    public DbSet<PostAnswer> PostAnswers { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

    public PostServerDbContext(DbContextOptions<PostServerDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        PostServerConfigs.Configure(modelBuilder);
    }
}
