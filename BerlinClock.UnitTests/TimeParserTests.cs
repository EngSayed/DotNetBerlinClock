using System;
using BerlinClock.Classes;
using BerlinClock.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace BerlinClock.UnitTests
{
    [TestFixture]
    public class TimeParserTests
    {
        private ITimeParser _timeParser;

        [SetUp]
        public void Setup()
        {
            _timeParser = new TimeParser();
        }

        [Test]
        public void ShouldThrowExceptionWhenNullString()
        {
            Assert.Throws<ArgumentException>(() => _timeParser.Parse(null));
        }

        [Test]
        public void ShouldThrowExceptionWhenEmptyString()
        {
            Assert.Throws<ArgumentException>(() => _timeParser.Parse(string.Empty));
        }

        [Test]
        public void ShouldThrowExceptionWhenEmptyStringNotTimeFormat()
        {
            Assert.Throws<FormatException>(() => _timeParser.Parse("1252:01"));
        }

        [TestCase("23:59:59", 23, 59, 59)]
        [TestCase("24:00:00", 24, 0, 0)]
        [TestCase("13:0:0", 13, 0, 0)]
        public void ShouldReturnTimeWhenStringTimeFormat(string s, int hours, int minutes, int seconds)
        {
            var time = _timeParser.Parse(s);

            time.Hours.Should().Be(hours);
            time.Minutes.Should().Be(minutes);
            time.Seconds.Should().Be(seconds);
        }
    }
}
