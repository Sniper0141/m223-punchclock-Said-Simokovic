namespace M223PunchclockDotnet.Model
{
    public class Entry
    {
        public int Id { get; set; }
        
        public required DateTime CheckIn { get; set; }
        
        public required DateTime CheckOut { get; set; }
    }
}
