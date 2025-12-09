using Domain.Entity.PostServer.Post;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configs.PostServers;

public static class PostConfigs
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostQuestion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AskerId).HasMaxLength(36);
            entity.Property(e => e.Title).HasMaxLength(300);
            entity.Property(e => e.Content).HasMaxLength(5000);
        });

        modelBuilder.Entity<PostAnswer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).HasMaxLength(36);
            entity.Property(e => e.Content).HasMaxLength(5000);
        });

        modelBuilder.Entity<PostTag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Slug).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(1000);
        });
    }
}
