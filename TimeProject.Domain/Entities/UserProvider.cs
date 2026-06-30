namespace TimeProject.Domain.Entities;

public class UserProvider
{
    public int ProviderId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string ExternalId { get; set; } = string.Empty;
    public int UserId { get; set; }
}