using System.ComponentModel.DataAnnotations;

namespace PostServer.API.DTO.Post;

public record PostAnswerCreateDto([Required] string Content, [Required] Guid PostQuestionId);
