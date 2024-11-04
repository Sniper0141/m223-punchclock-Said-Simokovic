namespace M223PunchclockDotnet.Controllers;

public record EditedEntryData
{
    public DateTime? CheckIn { get; set; }
    
    public DateTime? CheckOut { get; set; }
}