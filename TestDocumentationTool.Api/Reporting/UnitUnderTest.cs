using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDocumentationTool.Api
{
    public class UnitUnderTest
    {
        private List<OperationUnderTest> _operationsUnderTest;
        public string ClassName { get; }
        public string Namespace { get; }

        public IReadOnlyCollection<OperationUnderTest> OperationsUnderTest => _operationsUnderTest;
        public bool HasInformation => _operationsUnderTest.Any(x => x.HasInformation);

        public UnitUnderTest(string @namespace, string className)
        {
            if (string.IsNullOrWhiteSpace(className)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(className));
            if (string.IsNullOrWhiteSpace(@namespace)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(@namespace));

            ClassName = className;
            Namespace = @namespace;
            _operationsUnderTest = new List<OperationUnderTest>();
        }

        public void AddOperationUnderTest(OperationUnderTest operationUnderTest) => _operationsUnderTest.AddIfNotNull(operationUnderTest);

        public void Compress()
        {
            _operationsUnderTest = _operationsUnderTest.Where(x => x.HasInformation).ToList();
        }
    }
}