using System;

namespace TestDocumentationTool.Api
{
    public class TestScenario
    {
        public string Name { get; }

        public TestScenario(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            Name = name;
        }
    }
}