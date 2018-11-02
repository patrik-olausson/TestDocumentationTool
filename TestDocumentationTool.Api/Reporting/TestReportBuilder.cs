using System;
using System.Collections.Generic;

namespace TestDocumentationTool.Api
{
    public class TestReportBuilder
    {
        public TestReport CreateReport(IReadOnlyCollection<TestAssemblyInfo> testAssemblyInfos)
        {
            var testReport = new TestReport();
            foreach (var testAssemblyInfo in testAssemblyInfos)
            {
                var extractor = GetTestInformationExtractor(testAssemblyInfo);
                var projectInfo = extractor.TryExtractProjectInfo();
                if (projectInfo != null)
                {
                    testReport.AddProject(projectInfo);
                }
            }

            return testReport;
        }

        private ITestInformationExtractor GetTestInformationExtractor(TestAssemblyInfo testAssemblyInfo)
        {
            if (testAssemblyInfo == null) throw new ArgumentNullException(nameof(testAssemblyInfo));

            if (testAssemblyInfo.TestFramework == "xUnit")
            {
                return new XUnitTestInformationExtractor(testAssemblyInfo);
            }

            throw new NotImplementedException($"There is no implementation for test framework {testAssemblyInfo.TestFramework}");
        }
    }
}