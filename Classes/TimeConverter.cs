using System;
using System.Collections.Generic;
using System.Linq;
using BerlinClock.Interfaces;

namespace BerlinClock.Classes
{
    internal class TimeConverter : ITimeConverter
    {
        private readonly ITimeParser _timeParser;
        private readonly TimeValidator _timeValidator;
        private readonly ITimePartParser[] _lightParsers;

        public TimeConverter(ITimeParser timeParser, TimeValidator timeValidator)
        {
            _timeParser = timeParser;
            _timeValidator = timeValidator;

            _lightParsers = new ITimePartParser[]
            {
                new TimePartParser(new List<string>
                {
                    LampState.Yellow
                }, time => time.Seconds % 2 == 0 ? 1 : 0),
                new TimePartParser(new List<string>
                {
                    LampState.Red, LampState.Red, LampState.Red, LampState.Red
                }, time => time.Hours / 5),
                new TimePartParser(new List<string>
                {
                    LampState.Red, LampState.Red, LampState.Red, LampState.Red
                }, time => time.Hours % 5),
                new TimePartParser(new List<string>
                {
                    LampState.Yellow, LampState.Yellow, LampState.Red,
                    LampState.Yellow, LampState.Yellow, LampState.Red,
                    LampState.Yellow, LampState.Yellow, LampState.Red,
                    LampState.Yellow, LampState.Yellow
                }, time => time.Minutes / 5),
                new TimePartParser(new List<string>
                {
                    LampState.Yellow, LampState.Yellow, LampState.Yellow, LampState.Yellow
                }, time => time.Minutes % 5)
            };
        }

        public string ConvertTime(string aTime)
        {
            var time = _timeParser.Parse(aTime);
            var validationResult = _timeValidator.Validate(time);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(failure => 
                    $"Property {failure.PropertyName} failed validation. Error was: {failure.ErrorMessage}").ToList();

                if (errorMessages.Any())
                {
                    throw new ValidationErrorException(string.Join(Environment.NewLine, errorMessages));
                }
            }

            IBerlinTimeConverter converter = new BerlinTimeConverter(_lightParsers);

            return converter.Convert(time);
        }
    }
}