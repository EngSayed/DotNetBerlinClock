using System;
using BerlinClock.Interfaces;

namespace BerlinClock.Classes
{
    internal class TimeParser : ITimeParser
    {
        public ITime Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentException("Time cannot be empty nor null", s);
            }

            // We can use TimeSpan to parse even with hour "24" which will be parsed as "0" and add 1 day
            if (!TimeSpan.TryParse(s, out var timeSpan))
            {
                throw new FormatException($"The passed input {s} is not a correct time format");
            }

            var timeArray = s.Split(':');
            var timeHours = int.Parse(timeArray[0]);
            var timeMinutes = int.Parse(timeArray[1]);
            var timeSeconds = int.Parse(timeArray[2]);

            if (timeSpan.Hours != timeHours || timeSpan.Minutes != timeMinutes || timeSpan.Seconds != timeSeconds)
            {
                if (timeHours != 24 || timeMinutes != 0 || timeSeconds != 0)
                {
                    throw new FormatException($"The passed input {s} is not a correct time format");
                }
            }

            return new BerlinTime(timeHours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}