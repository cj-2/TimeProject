using System.Net;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Domain.Entities;

public class UserAccessLog
{
    public int LogId { get; set; }
    public IPAddress? ClientIp { get; set; }
    public string UserAgent { get; set; } = string.Empty;
    public int UserId { get; set; }
    public AccessType Type { get; set; }
    public ProviderType? Provider { get; set; }
    public DateTime AccessedAt { get; set; }
}