namespace TestDocumentationTool.Api
{
    public interface ITestInformationExtractor
    {
        ProjectInfo TryExtractProjectInfo();
    }
}