namespace M223PunchclockDotnet.DataTransfer;

public record EditedEntryData
{
    public DateTime? CheckIn { get; set; }
    
    public DateTime? CheckOut { get; set; }
}