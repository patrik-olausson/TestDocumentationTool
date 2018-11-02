using System;
using System.Collections.Generic;
using ToolboxForTesting;
using Xunit;

namespace ExtensionMethodsTests
{
    public class HasValue
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void GivenAValueThatIsConsideredToBeVoid_ThenItReturnsFalse(string value)
        {
            var result = value.HasValue();

            Assert.False(result);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("a")]
        [InlineData("A")]
        [InlineData("Some text")]
        [InlineData("  spaces  ")]
        public void GivenAValueThatIsConsideredToBeValid_ThenItReturnsTrue(string value)
        {
            var result = value.HasValue();

            Assert.True(result);
        }
    }

    public class ToDelimitedString
    {
        [Fact]
        public void WhenCalledWithEmptyList_ThenItReturnsEmptyString()
        {
            var list = new List<string>();

            var result = list.ToDelimitedString();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WhenCalledWithNull_ThenItReturnsEmptyString()
        {
            List<string> list = null;

            var result = list.ToDelimitedString();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void WhenCalledWithListOfOneValue_ThenItReturnsValueWithoutSemiColon()
        {
            var list = new List<string> { "Value1" };

            var result = list.ToDelimitedString();

            Assert.Equal("Value1", result);
        }

        [Fact]
        public void WhenCalledWithListOfTwoValues_ThenItReturnsValuesSeparatedBySemiColon()
        {
            var list = new List<string> { "Value1", "Value2" };

            var result = list.ToDelimitedString();

            Assert.Equal("Value1;Value2", result);
        }

        [Fact]
        public void WhenCalledWithListOfTwoValuesAndExplicitlyUsingCommaAsDelimiter_ThenItReturnsValuesSeparatedByComma()
        {
            var list = new List<string> { "Value1", "Value2" };

            var result = list.ToDelimitedString(",");

            Assert.Equal("Value1,Value2", result);
        }

        [Fact]
        public void WhenCalledWithListOfFiveValues_ThenItReturnsValuesSeparatedWithSemiColon()
        {
            var list = new List<string> { "1", "2", "3", "4", "5" };

            var result = list.ToDelimitedString();

            Assert.Equal("1;2;3;4;5", result);
        }
    }

    public class EqualsIgnoreCase
    {
        [Theory]
        [InlineData("Hello", "hello", true)]
        [InlineData("HELLO", "hello", true)]
        [InlineData("HELLO", "World", false)]
        [InlineData("HELLO", null, false)]
        [InlineData(null, null, true)]
        public void GivenComparison_ThenItReturnsTrue(
            string value1,
            string value2,
            bool expectedResult)
        {
            var result = value1.EqualsIgnoreCase(value2);

            Assert.Equal(expectedResult, result);
        }
    }
}