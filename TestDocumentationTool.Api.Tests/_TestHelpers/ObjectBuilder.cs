namespace TestDocumentationTool.Api.Tests._TestHelpers
{
    public class ObjectBuilder
    {
        public static TestReport CreateTestReport(params ProjectInfo[] projectInfos)
        {
            var testReport = new TestReport();
            projectInfos.ForEach(x => testReport.AddProject(x));

            return testReport;
        }

        public static ProjectInfo CreateProjectInfo(
            string name = "DefaultName",
            params UnitUnderTest[] unitsUnderTest)
        {
            var projectInfo = new ProjectInfo(name);
            unitsUnderTest.ForEach(x => projectInfo.AddUnitUnderTest(x));

            return projectInfo;
        }

        public static UnitUnderTest CreateUnitUnderTest(
            string @namespace = "DefaultNamespace",
            string className = "DefaultClassName",
            params OperationUnderTest[] operationsUnderTest)
        {
            var unitUnderTest = new UnitUnderTest(@namespace, className);
            operationsUnderTest.ForEach(x => unitUnderTest.AddOperationUnderTest(x));

            return unitUnderTest;
        }

        public static OperationUnderTest CreateOperationUnderTest(
            string className = "DefaultClassName",
            string methodName = "DefaultMethodName",
            params TestScenario[] testScenarios)
        {
            var operationUnderTest = new OperationUnderTest(className, methodName);
            testScenarios.ForEach(x => operationUnderTest.AddTestScenario(x));

            return operationUnderTest;
        }

        public static TestScenario CreateTestScenario(
            string name = "DefaultName")
        {
            return new TestScenario(name);
        }
    }
}