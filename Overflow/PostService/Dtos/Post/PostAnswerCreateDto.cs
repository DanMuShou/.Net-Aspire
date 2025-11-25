using System.ComponentModel.DataAnnotations;

namespace PostService.Dtos.Post;

public record PostAnswerCreateDto(
    [Required] string Content,
    [Required] string PostQuestionId
);