using System.ComponentModel.DataAnnotations;

namespace M223PunchclockDotnet.DataTransfer;

public class NewEntryData
{
    [Required]
    public DateTime CheckIn { get; set; }
    
    [Required]
    public DateTime CheckOut { get; set; }
}