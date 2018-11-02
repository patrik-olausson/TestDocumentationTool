using System.Collections.Generic;
using System.IO;

namespace TestDocumentationTool.Api
{
    public interface IAssemblyLoader
    {
        IReadOnlyCollection<TestAssemblyInfo> LoadAssemblies(string rootDirectoryPath);
        IReadOnlyCollection<TestAssemblyInfo> LoadAssemblies(DirectoryInfo rootDirectoryInfo);
    }
}