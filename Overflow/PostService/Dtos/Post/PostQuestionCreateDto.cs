using System.ComponentModel.DataAnnotations;
using PostService.Validators.Post;

namespace PostService.Dtos.Post;

public record PostQuestionCreateDto(
    [Required] string Title,
    [Required] string Content,
    [Required, TagListValidator(1, 5)] List<string> Tags
);