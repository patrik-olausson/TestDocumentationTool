using System;
using System.Collections.Generic;
using System.IO;

namespace TestDocumentationTool.Api
{
    /// <summary>
    /// This class is a result of starting to go one way when thinking about
    /// how the error handling should work when loading assemblies.
    /// It's not uncommon that exceptions like these ones are thrown but sometimes
    /// it is simply easier to use code that don't throw exceptions...
    /// This is way programming is hard, there are small decisions at every corner
    /// and most decisions will have an impact :)
    /// </summary>
    public class AssemblyLoaderThatRequiresValidDirectory : IAssemblyLoader
    {
        public IReadOnlyCollection<TestAssemblyInfo> LoadAssemblies(string rootDirectoryPath)
        {
            return LoadAssemblies(new DirectoryInfo(rootDirectoryPath));
        }

        public IReadOnlyCollection<TestAssemblyInfo> LoadAssemblies(DirectoryInfo rootDirectoryInfo)
        {
            if (rootDirectoryInfo == null) throw new ArgumentNullException(nameof(rootDirectoryInfo));
            if(!rootDirectoryInfo.Exists) throw new ArgumentException("Directory does not exist.");

            //Implementation not continued...
            return null;
        }
    }
}