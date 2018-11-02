using System.Diagnostics.CodeAnalysis;
using System.IO;
using ApprovalTests.Reporters;
using TestDocumentationTool.Api;
using ToolboxForTesting;
using ToolboxForTesting.ApprovalTestHelpers;
using Xunit;

namespace Reporting.TestReportBuilderTests
{
    [ExcludeFromCodeCoverage]
    public class CreateReport : TestReportBuilderTestharness
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void GivenAListOfAssembliesThatContainsASetOfKnownTests_ThenItReturnsAReportObjectContainingTheExpectedInformation()
        {
            var testAssemblyInfos = new[]
            {
                CreateTestAssemblyInfo("TestDocumentationTool.Api.Tests.dll"),
                CreateTestAssemblyInfo("ToolboxForTesting.Tests.dll")
            };
            var sut = CreateSut();

            var result = sut.CreateReport(testAssemblyInfos);

            ApprovalsHelper.Verify(
                result.ToJsonString().ReplaceDateTime(), 
                nameof(GivenAListOfAssembliesThatContainsASetOfKnownTests_ThenItReturnsAReportObjectContainingTheExpectedInformation));
        }
    }

    [ExcludeFromCodeCoverage]
    public class TestReportBuilderTestharness
    {
        protected TestReportBuilder CreateSut()
        {
            return new TestReportBuilder();
        }

        protected TestAssemblyInfo CreateTestAssemblyInfo(string nameOfTestFile)
        {
            var fileInfo = FileHelper.CreateTestFileInfo(nameOfTestFile, CreateTestDirectoryInfo());
            return new TestAssemblyInfo(fileInfo, "xUnit");
        }

        /// <summary>
        /// Creates a directory info for a directory name with the path relative to this
        /// test suite test file directory.
        /// </summary>
        protected DirectoryInfo CreateTestDirectoryInfo()
        {
            return FileHelper.CreateTestFilesDirectoryInfo($@"Reporting\TestReportBuilderTests\_TestFiles");
        }
    }
}