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
    public class BerlinTimeConverterTests
    {
        [Test]
        public void ShouldConvertTime()
        {
            const string expectedResult = "YYR";
            var parserMock = new Mock<ITimePartParser>();
            parserMock.Setup(parser => parser.Convert(It.IsAny<ITime>())).Returns(expectedResult);
            IEnumerable<ITimePartParser> parsers = new[]
            {
                parserMock.Object
            };
            IBerlinTimeConverter converter = new BerlinTimeConverter(parsers);
            var convert = converter.Convert(It.IsAny<ITime>());
            convert.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void ShouldConvertTimeWithNewLineBreak()
        {
            const string expectedResult1 = "Y";
            const string expectedResult2 = "YYRYO";

            var parserMock1 = new Mock<ITimePartParser>();
            parserMock1.Setup(parser => parser.Convert(It.IsAny<ITime>())).Returns(expectedResult1);

            var parserMock2 = new Mock<ITimePartParser>();
            parserMock2.Setup(parser => parser.Convert(It.IsAny<ITime>())).Returns(expectedResult2);

            IEnumerable<ITimePartParser> parsers = new[]
            {
                parserMock1.Object, parserMock2.Object
            };
            IBerlinTimeConverter converter = new BerlinTimeConverter(parsers);
            var convert = converter.Convert(It.IsAny<ITime>());

            convert.Should().BeEquivalentTo(string.Join(Environment.NewLine, expectedResult1, expectedResult2));
        }
    }
}