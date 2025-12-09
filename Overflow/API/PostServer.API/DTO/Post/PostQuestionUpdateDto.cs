using System.ComponentModel.DataAnnotations;
using PostServer.API.Validators;

namespace PostServer.API.DTO.Post;

public record PostQuestionUpdateDto(
    [Required] string Title,
    [Required] string Content,
    [Required, PostTagListValidator(1, 5)] List<string> Tags
);
