using System;
using Humanizer;

namespace TestDocumentationTool.Api.Output
{
    public class DefaultNameFormatter : INameFormatter
    {
        public string SpecialCasedWordToSentence(string pascalCasedValue)
        {
            return pascalCasedValue.Humanize(LetterCasing.Sentence);
        }

        public string CreateTestScenarioDescription(string testScenarioName, string nameOfmethodUnderTest = "")
        {
            var descriptionParts = testScenarioName.Split("_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (descriptionParts.Length == 2)
            {
                return CreateTestScenarioDescriptionForTritonaliteNamingStyle(nameOfmethodUnderTest, descriptionParts);
            }

            if (descriptionParts.Length == 3)
            {
                return CreateTestScenarioDescriptionForOsheroveNamingStyle(descriptionParts);
            }

            return SpecialCasedWordToSentence(testScenarioName);
        }

        private string CreateTestScenarioDescriptionForOsheroveNamingStyle(string[] descriptionParts)
        {
            var what = $"When operation {descriptionParts[0]} is performed";
            
            var when = descriptionParts[1];
            if (when.StartsWith("When", StringComparison.InvariantCultureIgnoreCase))
            {
                when = when.Replace("When", "Given");
            }
            else if (!when.StartsWith("Given", StringComparison.InvariantCultureIgnoreCase))
            {
                when = $"Given{when}";
            }
            

            var then = descriptionParts[2];
            if (!then.StartsWith("Then", StringComparison.InvariantCultureIgnoreCase))
            {
                then = $"Then{then}";
            }

            return $"{SpecialCasedWordToSentence(when)}\n{what}\n{SpecialCasedWordToSentence(then)}";
        }

        private string CreateTestScenarioDescriptionForTritonaliteNamingStyle(
            string nameOfmethodUnderTest,
            string[] descriptionParts)
        {
            var given = descriptionParts[0];
            if (!given.StartsWith("Given", StringComparison.InvariantCultureIgnoreCase))
            {
                given = $"Given{given}";
            }

            var then = descriptionParts[1];
            if (!then.StartsWith("Then", StringComparison.InvariantCultureIgnoreCase))
            {
                then = $"Then{then}";
            }

            var when = string.Empty;
            if (nameOfmethodUnderTest.HasValue())
            {
                when = $"When operation {nameOfmethodUnderTest} is performed";
                return $"{SpecialCasedWordToSentence(given)}\n{when}\n{SpecialCasedWordToSentence(then)}";
            }

            return $"{SpecialCasedWordToSentence(given)}\n{SpecialCasedWordToSentence(then)}";
        }
    }
}