namespace TestDocumentationTool.Api.Output
{
    public interface INameFormatter
    {
        string SpecialCasedWordToSentence(string pascalCasedValue);
        string CreateTestScenarioDescription(string testScenarioName, string nameOfmethodUnderTest = "");
    }
}