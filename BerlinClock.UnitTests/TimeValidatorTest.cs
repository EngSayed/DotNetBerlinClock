using BerlinClock.Classes;
using FluentAssertions;
using NUnit.Framework;

namespace BerlinClock.UnitTests
{
    [TestFixture]
    public class TimeValidatorTest
    {
        [SetUp]
        public void Setup()
        {
            _validator = new TimeValidator();
        }

        private TimeValidator _validator;
        
        [TestCase(24, 0, 1, false, 1)]
        [TestCase(24, 1, 0, false, 1)]
        [TestCase(25, 1, 0, false, 1)]
        [TestCase(13, 0, 60, false, 1)]
        [TestCase(15, 60, 1, false, 1)]
        [TestCase(24, 0, 60, false, 2)]
        public void ShouldHaveErrorWhenTimeNotValid(int hours, int minutes, int seconds, bool expectedValid, int expectedNumberOfErrors)
        {
            var time = new BerlinTime(hours, minutes, seconds);
            var validationResult = _validator.Validate(time);
            validationResult.IsValid.Should().Be(expectedValid);
            validationResult.Errors.Should().HaveCount(expectedNumberOfErrors);
        }

        [TestCase(13, 0, 59)]
        [TestCase(1, 59, 1)]
        [TestCase(24, 0, 0)]
        public void ShouldBeValid(int hours, int minutes, int seconds)
        {
            var time = new BerlinTime(hours, minutes, seconds);
            var validationResult = _validator.Validate(time);
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().HaveCount(0);
        }
    }
}