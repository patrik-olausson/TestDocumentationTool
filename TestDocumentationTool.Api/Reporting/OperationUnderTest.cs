using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDocumentationTool.Api
{
    public class OperationUnderTest
    {
        private readonly List<TestScenario> _testScenarios;
        public string ClassName { get; }
        public string MethodName { get; }

        public IReadOnlyCollection<TestScenario> TestScenarios => _testScenarios;
        public bool HasInformation => _testScenarios.Any();

        public OperationUnderTest(string className, string methodName)
        {
            if (string.IsNullOrWhiteSpace(className)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(className));
            if (string.IsNullOrWhiteSpace(methodName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));

            ClassName = className;
            MethodName = methodName;
            _testScenarios = new List<TestScenario>();
        }

        public void AddTestScenario(TestScenario testScenario) => _testScenarios.AddIfNotNull(testScenario);
        public void AddTestScenarios(IReadOnlyCollection<TestScenario> testScenarios) => _testScenarios.AddRange(testScenarios);
    }
}