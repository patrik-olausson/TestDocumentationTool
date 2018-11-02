using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace TestDocumentationTool.Api
{
    public class XUnitTestInformationExtractor : TestInformationExtractorBase
    {
        public XUnitTestInformationExtractor(TestAssemblyInfo testAssemblyInfo) : base(testAssemblyInfo)
        {
        }

        protected override IReadOnlyCollection<TestScenario> ExtractScenarios(MethodDefinition method)
        {
            if (method.CustomAttributes.Any(a => a.AttributeType.Name == "FactAttribute"))
            {
                return new[] {new TestScenario(method.Name)};
            }

            if (method.CustomAttributes.Any(a => a.AttributeType.Name == "TheoryAttribute"))
            {
                var inlineDataAttributes = method.CustomAttributes.Select(x => x.AttributeType.Name == "InlineDataAttribute").ToList();
                var testScenarios = new List<TestScenario>(inlineDataAttributes.Count);
                var i = 1;
                foreach (var inlineDataAttribute in inlineDataAttributes)
                {
                    testScenarios.Add(new TestScenario($"{method.Name}_{i++}"));
                }
                return testScenarios;
            }

            return new TestScenario[0];
        }
    }
}