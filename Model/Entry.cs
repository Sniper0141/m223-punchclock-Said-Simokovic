using System.ComponentModel.DataAnnotations.Schema;

namespace M223PunchclockDotnet.Model
{
    [Table("Entry")]
    public class Entry : DbModel
    {
        public required DateTime CheckIn { get; set; }
        
        public required DateTime CheckOut { get; set; }
        
        public required int CategoryId { get; set; }
        
        public Category Category { get; set; }

        public List<Tag> Tags { get; set; } = [];
    }
}
