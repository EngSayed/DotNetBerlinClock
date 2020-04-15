using BerlinClock.Interfaces;

namespace BerlinClock.Classes
{
    internal class BerlinTime : ITime
    {
        public int Hours { get; }
        public int Minutes { get; }
        public int Seconds { get; }

        public BerlinTime(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }
    }
}