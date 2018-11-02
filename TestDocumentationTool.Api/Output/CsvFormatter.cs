using System;
using System.Text;

namespace TestDocumentationTool.Api.Output
{
    public static class CsvFormatter
    {
        public static StringBuilder ToCsv(
            this TestReport testReport,
            INameFormatter nameFormatter,
            string separator = ";")
        {
            if (nameFormatter == null) throw new ArgumentNullException(nameof(nameFormatter));

            var sb = new StringBuilder();

            if (testReport != null)
            {
                sb.AppendHeaderRow(separator);
                foreach (var projectInfo in testReport.Projects)
                {
                    foreach (var unitUnderTest in projectInfo.UnitsUnderTest)
                    {
                        foreach (var operationUnderTest in unitUnderTest.OperationsUnderTest)
                        {
                            foreach (var testScenario in operationUnderTest.TestScenarios)
                            {
                                sb.AppendContentRow(
                                    projectInfo,
                                    unitUnderTest,
                                    operationUnderTest,
                                    testScenario,
                                    separator,
                                    nameFormatter);
                            }
                        }
                    }
                }
            }

            return sb;
        }

        internal static void AppendHeaderRow(this StringBuilder sb, string separator)
        {
            sb?.AppendLine($"Service{separator}Unit{separator}Operation{separator}Scenario");
        }

        internal static void AppendContentRow(
            this StringBuilder sb, 
            ProjectInfo projectInfo,
            UnitUnderTest unitUnderTest,
            OperationUnderTest operationUnderTest,
            TestScenario testScenario,
            string separator,
            INameFormatter nameFormatter)
        {
            sb?.Append($"{nameFormatter.SpecialCasedWordToSentence(projectInfo.Name)}{separator}");
            sb?.Append($"{nameFormatter.SpecialCasedWordToSentence(unitUnderTest.Namespace)}{separator}");
            sb?.Append($"{nameFormatter.SpecialCasedWordToSentence(operationUnderTest.ClassName)}{separator}");
            sb?.AppendLine($"\"{nameFormatter.CreateTestScenarioDescription(testScenario.Name, operationUnderTest.ClassName)}\"");
        }
    }
}