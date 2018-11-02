using System.Diagnostics.CodeAnalysis;
using TestDocumentationTool.Api;
using TestDocumentationTool.Api.Output;
using TestDocumentationTool.Api.Tests._TestHelpers;
using ToolboxForTesting;
using Xunit;

namespace Output.FileManagerTests
{
    [ExcludeFromCodeCoverage]
    public class CreateFile : FileManagerTestharness
    {
        [Fact]
        public void GivenAReportWithSomeTestScenarios_ThenItCreatesACsvFileSavedInTheSpecifiedOutputDirectory()
        {
            var testReport = CreateTestReportWithTwoDifferentUnitsHavingACoupleScenariosEach();
            var outputDirectory = FileHelper.CreateTestFilesDirectoryInfo(@"Output\FileManagerTests\_TestFiles\Output", true);
            var sut = CreateSut();

            var result = sut.CreateFile(testReport, outputDirectory, ".csv");

            Assert.True(result.Exists);
        }
    }

    [ExcludeFromCodeCoverage]
    public class FileManagerTestharness
    {
        protected FileManager CreateSut(
            INameFormatter nameOFormatter = null)
        {
            return new FileManager(nameOFormatter ?? new DefaultNameFormatter());
        }

        protected TestReport CreateTestReportWithTwoDifferentUnitsHavingACoupleScenariosEach()
        {
            //New testing style
            return ObjectBuilder.CreateTestReport(
                ObjectBuilder.CreateProjectInfo(
                    "ProjectName",
                    ObjectBuilder.CreateUnitUnderTest(
                        "ClassUnderTest",
                        "MethodBeingTested", 
                        ObjectBuilder.CreateOperationUnderTest(
                            "MethodBeingTested",
                            "GivenSomeScenario1_ItSomeExpectedResult1",
                            ObjectBuilder.CreateTestScenario("GivenSomeScenario1_ItSomeExpectedResult1")),
                        ObjectBuilder.CreateOperationUnderTest(
                            "MethodBeingTested",
                            "GivenSomeScenario2_ItSomeExpectedResult2",
                            ObjectBuilder.CreateTestScenario("GivenSomeScenario2_ItSomeExpectedResult2"))),
                    ObjectBuilder.CreateUnitUnderTest(
                        "ClassUnderTest2",
                        "SomeOtherMethodBeingTested",
                        ObjectBuilder.CreateOperationUnderTest(
                            "SomeOtherMethodBeingTested",
                            "GivenSomeScenario3_ItSomeExpectedResult3",
                            ObjectBuilder.CreateTestScenario("GivenSomeScenario3_ItSomeExpectedResult3")),
                        ObjectBuilder.CreateOperationUnderTest(
                            "MethodBeingTested",
                            "GivenSomeScenario4_ItSomeExpectedResult4",
                            ObjectBuilder.CreateTestScenario("GivenSomeScenario4_ItSomeExpectedResult4")))));
        }
    }
}