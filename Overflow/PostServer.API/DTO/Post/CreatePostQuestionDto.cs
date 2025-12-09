using System.ComponentModel.DataAnnotations;
using PostServer.API.Validators;

namespace PostServer.API.DTO.Post;

public record CreatePostQuestionDto(string Title, string Content, List<string> Tags);
