using Microsoft.EntityFrameworkCore;
using PostService.Models.Post;

namespace PostService.Data;

public class PostDbContext(DbContextOptions<PostDbContext> options) : DbContext(options)
{
    public DbSet<PostQuestion> PostQuestions => Set<PostQuestion>();
    public DbSet<PostAnswer> PostAnswers => Set<PostAnswer>();
    public DbSet<PostTag> PostTags => Set<PostTag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostQuestion>()
            .HasIndex(q => q.AskerId)
            .HasDatabaseName("IX_PostQuestion_AskerId");

        modelBuilder.Entity<PostQuestion>()
            .HasIndex(q => q.TagSlugs)
            .HasDatabaseName("IX_PostQuestion_TagSlugs");

        modelBuilder.Entity<PostAnswer>()
            .HasIndex(a => a.UserId)
            .HasDatabaseName("IX_PostAnswer_UserId");

        modelBuilder.Entity<PostAnswer>()
            .HasIndex(a => a.PostQuestionId)
            .HasDatabaseName("IX_PostAnswer_PostQuestionId");
    }
}
