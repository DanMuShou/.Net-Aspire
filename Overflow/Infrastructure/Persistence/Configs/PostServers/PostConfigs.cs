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
            entity.Property(e => e.Title).HasMaxLength(300);
            entity.Property(e => e.Content).HasMaxLength(5000);
            entity.Property(e => e.AskerDisplayName).HasMaxLength(300);
        });
    }
}
