using System.ComponentModel.DataAnnotations;

namespace PostServer.API.DTO.Post;

public record CreatePostAnswerDto(string Content, Guid PostQuestionId);
