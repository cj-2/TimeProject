using TimeProject.Domain.Entities.Enums;
using TimeProject.Domain.Errors;
using TimeProject.Domain.Utils;

namespace TimeProject.Domain.Entities;

public class User
{
    private string _email = string.Empty;
    private  string _timezone = string.Empty;
    public int UserId { get; init; }
    public string Name { get; set; } = string.Empty;

    public string Email
    {
        get => _email;
        init => UpdateEmail(value);
    }

    public UserRoleType UserRole { get; set; } = UserRoleType.Normal;

    public string Timezone
    {
        get => _timezone;
        init => UpdateTimeZone(value);
    }

    public bool IsActive { get; set; } = true;

    public void UpdateEmail(string email)
    {
        _email = email;
    }

    public void UpdateTimeZone(string tz)
    {
        var zone = TimeZones.All.FirstOrDefault(e => e.Equals(tz, StringComparison.InvariantCultureIgnoreCase));
        _timezone = zone ?? throw new DomainException(UserErrors.TimeZoneNotFound);
    }
}