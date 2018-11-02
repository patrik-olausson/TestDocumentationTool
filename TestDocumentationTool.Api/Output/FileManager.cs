using System;
using System.IO;
using System.Text;

namespace TestDocumentationTool.Api.Output
{
    public class FileManager
    {
        private readonly INameFormatter _nameFormatter;

        public FileManager(INameFormatter nameFormatter)
        {
            if (nameFormatter == null) throw new ArgumentNullException(nameof(nameFormatter));

            _nameFormatter = nameFormatter;
        }

        public FileInfo CreateFile(
            TestReport testReport, 
            DirectoryInfo outputDirectory, 
            string fileFormat,
            Encoding encoding = null)
        {
            if (testReport == null) throw new ArgumentNullException(nameof(testReport));
            if (outputDirectory == null) throw new ArgumentNullException(nameof(outputDirectory));
            if (!outputDirectory.Exists) throw new ArgumentException($"Output directory {outputDirectory.FullName} does not exist.", nameof(outputDirectory));
            if (string.IsNullOrWhiteSpace(fileFormat)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileFormat));

            var fileContent = GetFileContent(testReport, fileFormat);
            var fileName = Path.Combine(outputDirectory.FullName, $"TestReport_{testReport.DateCreated.ToFileTime()}{fileFormat}");
            var fileInfo= new FileInfo(fileName);
            var fileContentAsText = fileContent.ToString();
            File.WriteAllText(fileInfo.FullName, fileContentAsText, encoding ?? Encoding.UTF8);

            return fileInfo;
        }

        private StringBuilder GetFileContent(TestReport testReport, string fileFormat)
        {
            if (fileFormat == ".csv")
            {
                return testReport.ToCsv(_nameFormatter);
            }

            throw new ArgumentException($"File format {fileFormat} not supported!", nameof(fileFormat));
        }
    }
}