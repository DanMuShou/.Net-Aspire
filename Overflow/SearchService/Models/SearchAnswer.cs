using System.Text.Json.Serialization;

namespace SearchService.Models;

public class SearchAnswer
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("content")]
    public required string Content { get; set; }

    [JsonPropertyName("createdAt")]
    public long CreatedAt { get; set; }

    [JsonPropertyName("isAccepted")]
    public bool IsAccepted { get; set; }

    [JsonPropertyName("postQuestionId")]
    public required string PostQuestionId { get; set; }
}
