using System.ComponentModel.DataAnnotations;

namespace PostServer.API.DTO.Post;

public record UpdatePostAnswerDto(string Content, bool IsAccepted);
