using System;
using System.IO;

namespace TestDocumentationTool.Api
{
    public class TestAssemblyInfo
    {
        public string AssemblyName => AssemblyFileInfo.Name;
        public FileInfo AssemblyFileInfo { get; }
        public string TestFramework { get; }

        public TestAssemblyInfo(FileInfo assemblyFileInfo, string testFramework)
        {
            if (assemblyFileInfo == null) throw new ArgumentNullException(nameof(assemblyFileInfo));
            if (string.IsNullOrWhiteSpace(testFramework)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(testFramework));

            AssemblyFileInfo = assemblyFileInfo;
            TestFramework = testFramework;
        }

        public override string ToString()
        {
            return $"{AssemblyName} ({TestFramework}) - {AssemblyFileInfo.DirectoryName}";
        }
    }
}