using System.Text.Json.Serialization;

namespace HK.Payments.Core.Models;

public sealed record ErrorResponse()
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("internalErrorCode")]
    public string? InternalErrorCode { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("detail")]
    public string? Detail { get; set; }

    [JsonPropertyName("errors")]
    public List<string>? Errors { get; set; }
}
