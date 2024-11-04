namespace M223PunchclockDotnet.Controllers;

public record EditedEntryData
{
    public bool newCheckIn;
    public bool newCheckOut;
    
    public DateTime CheckIn
    {
        get => CheckIn;
        set
        {
            newCheckIn = true;
        }
    }
    
    public DateTime CheckOut
    {
        get => CheckOut;
        set
        {
            newCheckOut = true;
        }
    }
}