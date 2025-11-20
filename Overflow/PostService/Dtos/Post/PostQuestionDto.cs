using System.ComponentModel.DataAnnotations;

namespace PostService.Dtos.Post;

public record PostQuestionDto(
    [Required] string Id,
    [Required] string Title,
    [Required] string Content,
    [Required] string AskerId,
    [Required] string AskerDisplayName,
    [Required] int ViewCount,
    [Required] List<string> TagSlugs
);
