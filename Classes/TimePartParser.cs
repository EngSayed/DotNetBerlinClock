using System;
using System.Collections.Generic;
using System.Text;
using BerlinClock.Interfaces;

namespace BerlinClock.Classes
{
    internal class TimePartParser : ITimePartParser
    {
        private readonly IList<string> _lampStateList;
        private readonly Func<ITime, int> _lightFunc;

        public TimePartParser(IList<string> lampStateList, Func<ITime, int> lightFunc)
        {
            _lampStateList = lampStateList;
            _lightFunc = lightFunc;
        }

        public string Convert(ITime berlinTime)
        {
            var result = new StringBuilder();
            var lampsCount = _lightFunc(berlinTime);

            foreach (var lampState in _lampStateList)
            {
                result.Append(lampsCount > 0 ? lampState : LampState.Off);

                lampsCount--;
            }

            return result.ToString();
        }
    }
}