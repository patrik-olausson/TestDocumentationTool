using System.Diagnostics.CodeAnalysis;
using TestDocumentationTool.Api.Output;
using Xunit;

namespace Output.DefaultNameFormatterTests
{
    [ExcludeFromCodeCoverage]
    public class SpecialCasedWordToSentence : DefaultNameFormatterTestharness
    {
        [Fact]
        public void GivenAPascalCasedString_ThenItReturnsTheStringAsARegularText()
        {
            var value = "ThisIsAPascalCasedString";
            var sut = CreateSut();

            var result = sut.SpecialCasedWordToSentence(value);

            Assert.Equal("This is a pascal cased string", result);
        }

        [Fact]
        public void GivenACamelCasedString_ThenItReturnsTheStringAsARegularText()
        {
            var value = "thisIsACamelCasedString";
            var sut = CreateSut();

            var result = sut.SpecialCasedWordToSentence(value);

            Assert.Equal("This is a camel cased string", result);
        }

        [Fact]
        public void GivenAKebabCasedString_ThenItReturnsTheStringAsARegularText()
        {
            var value = "this-is-a-kebab-cased-string";
            var sut = CreateSut();

            var result = sut.SpecialCasedWordToSentence(value);

            Assert.Equal("This is a kebab cased string", result);
        }

        [Fact]
        public void GivenASnakeCasedString_ThenItReturnsTheStringAsARegularText()
        {
            var value = "this_is_a_snake_cased_string";
            var sut = CreateSut();

            var result = sut.SpecialCasedWordToSentence(value);

            Assert.Equal("This is a snake cased string", result);
        }
    }

    [ExcludeFromCodeCoverage]
    public class CreateTestScenarioDescription : DefaultNameFormatterTestharness
    {
        [Fact]
        public void GivenATestScenarioNameWithGivenAndThen_ThenItCreatesAReadableText()
        {
            var testScenarioName = "GivenAReportWithSomeTestScenarios_ThenItCreatesACsvFileSavedInTheSpecifiedOutputDirectory";
            var sut = CreateSut();

            var result = sut.CreateTestScenarioDescription(testScenarioName);

            Assert.Equal("Given a report with some test scenarios\nThen it creates a csv file saved in the specified output directory", result);
        }

        [Fact]
        public void GivenATestScenarioNameWithTwoPartsButNoExplicitGivenAndThen_ThenItCreatesAReadableTextContainingGivenAndThen()
        {
            var testScenarioName = "AReportWithSomeTestScenarios_ItCreatesACsvFileSavedInTheSpecifiedOutputDirectory";
            var sut = CreateSut();

            var result = sut.CreateTestScenarioDescription(testScenarioName);

            Assert.Equal("Given a report with some test scenarios\nThen it creates a csv file saved in the specified output directory", result);
        }

        [Fact]
        public void GivenATestScenarioNameWithTwoPartsButNoExplicitGiven_ThenItCreatesAReadableTextContainingGivenAndThen()
        {
            var testScenarioName = "AReportWithSomeTestScenarios_thenItCreatesACsvFileSavedInTheSpecifiedOutputDirectory";
            var sut = CreateSut();

            var result = sut.CreateTestScenarioDescription(testScenarioName);

            Assert.Equal("Given a report with some test scenarios\nThen it creates a csv file saved in the specified output directory", result);
        }

        [Fact]
        public void GivenATestScenarioNameWithTwoPartsButNoExplicitThen_ThenItCreatesAReadableTextContainingGivenAndThen()
        {
            var testScenarioName = "givenAReportWithSomeTestScenarios_ItCreatesACsvFileSavedInTheSpecifiedOutputDirectory";
            var sut = CreateSut();

            var result = sut.CreateTestScenarioDescription(testScenarioName);

            Assert.Equal("Given a report with some test scenarios\nThen it creates a csv file saved in the specified output directory", result);
        }

        [Fact]
        public void GivenATestScenarioNameWithTwoPartsAndTheMethodName_ThenItCreatesAReadableTextContainingGivenWhenAndThen()
        {
            var testScenarioName = "givenAReportWithSomeTestScenarios_ItCreatesACsvFileSavedInTheSpecifiedOutputDirectory";
            var sut = CreateSut();

            var result = sut.CreateTestScenarioDescription(testScenarioName, "CreateFile");

            Assert.Equal("Given a report with some test scenarios\nWhen operation CreateFile is performed\nThen it creates a csv file saved in the specified output directory", result);
        }

        [Fact]
        public void GivenATestScenarioNameWithTheOsheroveNamingStyleWithoutExplicitWhenAndThen_ThenItCreatesAReadableTextContainingGivenWhenAndThen()
        {
            var testScenarioName = "CreateFile_AReportWithSomeTestScenarios_ItCreatesACsvFileSavedInTheSpecifiedOutputDirectory";
            var sut = CreateSut();

            var result = sut.CreateTestScenarioDescription(testScenarioName);

            Assert.Equal("Given a report with some test scenarios\nWhen operation CreateFile is performed\nThen it creates a csv file saved in the specified output directory", result);
        }

        [Fact]
        public void GivenATestScenarioNameWithTheOsheroveNamingStyleWithExplicitWhenAndThen_ThenItCreatesAReadableTextContainingGivenWhenAndThen()
        {
            var testScenarioName = "CreateFile_WhenAReportWithSomeTestScenarios_ThenItCreatesACsvFileSavedInTheSpecifiedOutputDirectory";
            var sut = CreateSut();

            var result = sut.CreateTestScenarioDescription(testScenarioName);

            Assert.Equal("Given a report with some test scenarios\nWhen operation CreateFile is performed\nThen it creates a csv file saved in the specified output directory", result);
        }
    }

    [ExcludeFromCodeCoverage]
    public class DefaultNameFormatterTestharness
    {
        protected DefaultNameFormatter CreateSut()
        {
            return new DefaultNameFormatter();
        }
    }
}