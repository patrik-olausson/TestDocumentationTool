using System.Collections.Generic;
using Xunit;

namespace ToolboxForTesting.Tests
{
    public class OsheroveExtensionMethodsTests
    {
        #region HasValue

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void HasValue_GivenAValueThatIsConsideredToBeVoid_ThenItReturnsFalse(string value)
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
        public void HasValue_GivenAValueThatIsConsideredToBeValid_ThenItReturnsTrue(string value)
        {
            var result = value.HasValue();

            Assert.True(result);
        }

        [Fact]
        public void ToDelimitedString_WhenCalledWithNull_ThenItReturnsEmptyString()
        {
            List<string> list = null;

            var result = list.ToDelimitedString();

            Assert.Equal(string.Empty, result);
        }

        #endregion

        #region ToDelimitedString

        [Fact]
        public void ToDelimitedString_WhenCalledWithEmptyList_ThenItReturnsEmptyString()
        {
            var list = new List<string>();

            var result = list.ToDelimitedString();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ToDelimitedString_WhenCalledWithListOfOneValue_ThenItReturnsValueWithoutSemiColon()
        {
            var list = new List<string> { "Value1" };

            var result = list.ToDelimitedString();

            Assert.Equal("Value1", result);
        }

        [Fact]
        public void ToDelimitedString_WhenCalledWithListOfTwoValues_ThenItReturnsValuesSeparatedBySemiColon()
        {
            var list = new List<string> { "Value1", "Value2" };

            var result = list.ToDelimitedString();

            Assert.Equal("Value1;Value2", result);
        }

        [Fact]
        public void ToDelimitedString_WhenCalledWithListOfFiveValues_ThenItReturnsValuesSeparatedWithSemiColon()
        {
            var list = new List<string> { "1", "2", "3", "4", "5" };

            var result = list.ToDelimitedString();

            Assert.Equal("1;2;3;4;5", result);
        }

        #endregion
    }
}