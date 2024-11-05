using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace M223PunchclockDotnet.Model;

[Table("Tag")]
public class Tag : DbModel
{
    [MaxLength(255)]
    public required string Title { get; set; }
}