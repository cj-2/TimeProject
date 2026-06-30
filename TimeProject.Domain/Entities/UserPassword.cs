namespace TimeProject.Domain.Entities;

public class UserPassword
{
    public int PasswordId { get; set; }
    public string Password { get; set; } = string.Empty;
    public int UserId { get; set; }
    public bool IsActive { get; set; }
}