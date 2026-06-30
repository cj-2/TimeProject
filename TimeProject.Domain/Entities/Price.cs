namespace TimeProject.Domain.Entities;

public class Price
{
    public int PriceId { get; set; }
    public int? ProjectId { get; set; }
    public int? RecordId { get; set; }
    public decimal Value { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int UserId { get; set; }
}