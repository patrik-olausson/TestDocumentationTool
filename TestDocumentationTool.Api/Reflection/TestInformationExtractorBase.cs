using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace TestDocumentationTool.Api
{
    public abstract class TestInformationExtractorBase : ITestInformationExtractor
    {
        protected readonly TestAssemblyInfo TestAssemblyInfo;

        protected TestInformationExtractorBase(TestAssemblyInfo testAssemblyInfo)
        {
            if (testAssemblyInfo == null) throw new ArgumentNullException(nameof(testAssemblyInfo));

            TestAssemblyInfo = testAssemblyInfo;
        }
        
        public ProjectInfo TryExtractProjectInfo()
        {
            var projectInfo = new ProjectInfo(TestAssemblyInfo.AssemblyName);
            foreach (var publicClass in GetPublicClassesInTestAssembly())
            {
                var unitUnderTest = projectInfo.GetOrCreateUnitUnderTest(publicClass);
                foreach (var method in publicClass.GetMethods())
                {
                    var operationUnderTest = new OperationUnderTest(publicClass.Name, method.Name);
                    var testScenarios = ExtractScenarios(method);
                    operationUnderTest.AddTestScenarios(testScenarios);
                    unitUnderTest.AddOperationUnderTest(operationUnderTest);
                }
            }

            projectInfo.Compress();
            if (projectInfo.HasInformation)
            {
                return projectInfo;
            }
            return null;
        }
        
        private IEnumerable<TypeDefinition> GetPublicClassesInTestAssembly()
        {
            var module = ModuleDefinition.ReadModule(TestAssemblyInfo.AssemblyFileInfo.FullName);
            var publicClasses = module.GetTypes().Where(t => t.IsPublic && t.IsClass);

            return publicClasses;
        }

        protected abstract IReadOnlyCollection<TestScenario> ExtractScenarios(MethodDefinition method);
    }
}