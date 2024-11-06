using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace M223PunchclockDotnet.Model;

[Table("Entry")]
public class Category : DbModel
{
    [MaxLength(255)]
    public required string Title { get; set; }
    
    public List<Entry> Entries { get; set; } = [];
}