using System;
using System.Collections.Generic;
using System.Linq;
using BerlinClock.Interfaces;

namespace BerlinClock.Classes
{
    internal class BerlinTimeConverter : IBerlinTimeConverter
    {
        private readonly IEnumerable<ITimePartParser> _lightParsers;

        public BerlinTimeConverter(IEnumerable<ITimePartParser> lightParsers)
        {
            _lightParsers = lightParsers;
        }

        public string Convert(ITime berlinTime)
        {
            var result = _lightParsers.Select(lightParser => lightParser.Convert(berlinTime)).ToList();

            return string.Join(Environment.NewLine, result);
        }
    }
}