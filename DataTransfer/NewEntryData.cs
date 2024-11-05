using System.ComponentModel.DataAnnotations;

namespace M223PunchclockDotnet.DataTransfer;

public class NewEntryData
{
    public int Id { get; set; }
    
    [Required]
    public DateTime CheckIn { get; set; }
    
    [Required]
    public DateTime CheckOut { get; set; }
}