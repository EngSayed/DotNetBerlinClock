using System.Collections.Generic;
using BerlinClock.Classes;
using BerlinClock.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BerlinClock.UnitTests
{
    [TestFixture]
    public class TimePartParserTests
    {
        public class TestClass
        {
            public IList<string> LampStateList { get; set; }
            public int LightsToConvert { get; set; }
            public string ExpectedOutput { get; set; }
        }

        private static List<TestClass> _testCases = new List<TestClass>
        {
            new TestClass
            {
                LampStateList = new List<string>
                {
                    LampState.Yellow, LampState.Red, LampState.Yellow
                },
                LightsToConvert = 3,
                ExpectedOutput = "YRY"
            },
            new TestClass
            {
                LampStateList = new List<string>
                {
                    LampState.Yellow, LampState.Yellow, LampState.Yellow, LampState.Yellow
                },
                LightsToConvert = 3,
                ExpectedOutput = "YYYO"
            },
            new TestClass
            {
                LampStateList = new List<string>
                {
                    LampState.Yellow, LampState.Red, LampState.Yellow, LampState.Yellow, LampState.Red, LampState.Red
                },
                LightsToConvert = 10,
                ExpectedOutput = "YRYYRR"
            }
        };

        [TestCaseSource(nameof(_testCases))]
        public void ShouldConvertBasedOnLightToConvertFunc(TestClass testCase)
        {
            ITimePartParser parser = new TimePartParser(testCase.LampStateList, _ => testCase.LightsToConvert);
            var result = parser.Convert(It.IsAny<ITime>());
            result.Should().BeEquivalentTo(testCase.ExpectedOutput);
        }
    }
}