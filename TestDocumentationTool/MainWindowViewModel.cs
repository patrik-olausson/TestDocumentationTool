using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using TestDocumentationTool.Api;
using TestDocumentationTool.Api.Output;

namespace TestDocumentationTool
{
    public class MainWindowViewModel
    {
        private readonly IAssemblyLoader _assemblyLoader;
        
        public MainWindowViewModel(
            IAssemblyLoader assemblyLoader)
        {
            if (assemblyLoader == null) throw new ArgumentNullException(nameof(assemblyLoader));

            _assemblyLoader = assemblyLoader;
            TestAssemblies = new ObservableCollection<TestAssemblyInfo>();
        }
        public ObservableCollection<TestAssemblyInfo> TestAssemblies { get; }

        public void LoadTestAssemblies(string directoryPath)
        {
            TestAssemblies.Clear();
            var testAssemblyInfos = _assemblyLoader.LoadAssemblies(directoryPath);
            testAssemblyInfos.ForEach(testAssemblyInfo => TestAssemblies.Add(testAssemblyInfo));
        }

        public void CreateCsvFile(string directoryPath)
        {
            var testReportBuilder = new TestReportBuilder();
            var fileManager = new FileManager(new DefaultNameFormatter());
            var testReport = testReportBuilder.CreateReport(TestAssemblies);
            fileManager.CreateFile(testReport, new DirectoryInfo(directoryPath), ".csv");
        }
    }
}