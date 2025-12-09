using System.ComponentModel.DataAnnotations;
using PostServer.API.Validators;

namespace PostServer.API.DTO.Post;

public record UpdatePostQuestionDto(string Title, string Content, List<string> Tags);
