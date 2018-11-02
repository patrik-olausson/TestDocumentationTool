using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace TestDocumentationTool.Api
{
    public class AssemblyLoaderThatAlwaysReturnSomething : IAssemblyLoader
    {
        public IReadOnlyCollection<TestAssemblyInfo> LoadAssemblies(string rootDirectoryPath)
        {
            return LoadAssemblies(new DirectoryInfo(rootDirectoryPath));
        }

        public IReadOnlyCollection<TestAssemblyInfo> LoadAssemblies(DirectoryInfo rootDirectoryInfo)
        {
            if (rootDirectoryInfo == null) return new TestAssemblyInfo[0];
            if (!rootDirectoryInfo.Exists) return new TestAssemblyInfo[0];

            var excludedDirectories = new[] {"packages", "obj", "Release", ".git", ".nuget", ".vs"};

            var subDirectories = rootDirectoryInfo.GetDirectories("*", SearchOption.AllDirectories).ToList();
            subDirectories.Add(rootDirectoryInfo);
            var testAssemblyCandidates = new List<FileInfo>();
            foreach (var subDirectory in subDirectories)
            {
                if(excludedDirectories.Contains(subDirectory.Name))
                    continue;
                
                testAssemblyCandidates.AddRange(subDirectory.GetFiles("*.dll", SearchOption.TopDirectoryOnly).Where(x => x.Name.Contains("Test")));
            }

            var assembliesThatContainsTests = testAssemblyCandidates
                .Distinct(new LambdaComparer<FileInfo>((a, b) => a.Name == b.Name))
                .Select(TryCreateTestAssemblyInfo)
                .Where(x => x != null)
                .ToList();
            
            return assembliesThatContainsTests;
        }

        private TestAssemblyInfo TryCreateTestAssemblyInfo(FileInfo fileInfo)
        {
            var module = ModuleDefinition.ReadModule(fileInfo.FullName);
            var publicClasses = module.GetTypes().Where(t => t.IsPublic && t.IsClass);
            foreach (var publicClass in publicClasses)
            {
                var methods = publicClass.GetMethods();
                foreach (var method in methods)
                {
                    if (method.CustomAttributes.Any(
                        a => a.AttributeType.Name == "FactAttribute" ||
                             a.AttributeType.Name == "TheoryAttribute"))
                        return new TestAssemblyInfo(fileInfo, "xUnit");
                }
            }

            return null;
        }
    }
}