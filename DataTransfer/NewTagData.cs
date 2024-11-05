using System.ComponentModel.DataAnnotations;

namespace M223PunchclockDotnet.DataTransfer;

public class NewTagData
{
    public int Id { get; set; }
    
    [Required]
    public required string Title { get; set; }
}