using System.ComponentModel.DataAnnotations;

namespace PostService.Dtos.Post;

public record PostAnswerUpdateDto(
    [Required] string Content,
    [Required] bool IsAccepted
);