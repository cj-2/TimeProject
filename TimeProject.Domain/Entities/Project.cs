namespace TimeProject.Domain.Entities;

public class Project
{
    public int ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UserId { get; set; }
}