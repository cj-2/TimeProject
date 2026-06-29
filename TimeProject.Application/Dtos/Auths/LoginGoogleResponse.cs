using System.Text.Json.Serialization;

namespace TimeProject.Application.Dtos.Auths;

public class LoginGoogleResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    [JsonPropertyName("verified_email")] public bool VerifiedEmail { get; set; }
}