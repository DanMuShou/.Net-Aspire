using System.ComponentModel.DataAnnotations;

namespace PostServer.API.DTO.Post;

public record PostAnswerUpdateDto([Required] string Content, [Required] bool IsAccepted);
