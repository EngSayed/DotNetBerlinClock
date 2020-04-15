namespace BerlinClock.Interfaces
{
    public interface ITime
    {
        int Hours { get; }
        int Minutes { get; }
        int Seconds { get; }
    }
}