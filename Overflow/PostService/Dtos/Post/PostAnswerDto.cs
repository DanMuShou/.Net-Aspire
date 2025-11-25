using System.ComponentModel.DataAnnotations;

namespace PostService.Dtos.Post;

public record PostAnswerDto(
    [Required] string Id,
    [Required] string Content,
    [Required] DateTime CreatedAt,
    [Required] bool IsAccepted,
    [Required] string PostQuestionId
);
