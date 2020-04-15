using System;
using System.Collections.Generic;
using BerlinClock.Classes;
using BerlinClock.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BerlinClock.UnitTests
{
    [TestFixture]
    public class TimeConverterTests
    {
        public class TestClass
        {
            public string Time { get; set; }
            public string Output { get; set; }
        }

        [TestCase("25:00:00")]
        [TestCase("23:60:00")]
        [TestCase("23:00:60")]
        [TestCase("24:01:00")]
        [TestCase("24:00:01")]
        [TestCase("24:00")]
        public void ShouldThrowFormatExceptionForInvalidTime(string time)
        {
            var timeValidatorMock = new Mock<TimeValidator>();
            ITimeConverter converter = new TimeConverter(new TimeParser(), timeValidatorMock.Object);

            Assert.Throws<FormatException>(() => converter.ConvertTime(time));
        }

        [TestCaseSource(nameof(_testCases))]
        public void ShouldConvertTime(TestClass testCase)
        {
            ITimeConverter converter = new TimeConverter(new TimeParser(), new TimeValidator());

            var convertTime = converter.ConvertTime(testCase.Time);
            convertTime.Should().BeEquivalentTo(testCase.Output);
        }

        private static List<TestClass> _testCases = new List<TestClass>
        {
            new TestClass
            {
                Time = "24:00:00",
                Output = @"Y
RRRR
RRRR
OOOOOOOOOOO
OOOO"
            },
            new TestClass
            {
                Time = "23:59:59",
                Output = @"O
RRRR
RRRO
YYRYYRYYRYY
YYYY"
            }
        };
    }
}