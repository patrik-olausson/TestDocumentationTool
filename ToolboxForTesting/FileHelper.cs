using System;
using System.IO;
using System.Text;

namespace ToolboxForTesting
{
    public class FileHelper
    {
        /// <summary>
        /// The base directory at the time when the test is run. 
        /// Files could be moved during build and the testrunner
        /// uses files in a specific (but dynamically named) test folder.
        /// </summary>
        public static string TestRunBaseDirectory => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath">The directory path relative to the test ruen base directory.</param>
        /// <param name="createDirectoryIfNotExists"></param>
        /// <returns></returns>
        public static DirectoryInfo CreateTestFilesDirectoryInfo(
            string directoryPath, 
            bool createDirectoryIfNotExists = false)
        {
            if (string.IsNullOrWhiteSpace(directoryPath)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(directoryPath));

            var testDirectoryPath = Path.Combine(TestRunBaseDirectory, directoryPath);
            var testFilesDirectory = new DirectoryInfo(testDirectoryPath);
            if (createDirectoryIfNotExists && !testFilesDirectory.Exists)
            {
                testFilesDirectory.Create();
            }

            return testFilesDirectory;
        }

        public static FileInfo CreateTestFileInfo(
            string fileName, 
            DirectoryInfo testFilesDirectoryInfo,
            bool createFile = false)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileName));
            if (testFilesDirectoryInfo == null) throw new ArgumentNullException(nameof(testFilesDirectoryInfo));
            if (!testFilesDirectoryInfo.Exists) throw new ArgumentException($"Directory {testFilesDirectoryInfo.Name} does not exist. Unable to load test file {fileName}.");

            var testFilePath = Path.Combine(testFilesDirectoryInfo.FullName, fileName);
            var testFile = new FileInfo(testFilePath);
            if (createFile && !testFile.Exists)
            {
                testFile.Create();
            }

            return testFile;
        }

        /// <summary>
        /// Tries to load all the content from a specified file that contains JSON
        /// and deserialize it into an object of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameOfTestFile"></param>
        /// <param name="testFileDirectory"></param>
        /// <returns></returns>
        public static T CreateFromJsonFile<T>(
            string nameOfTestFile, 
            string testFileDirectory = "_TestFiles") where T : class
        {
            return CreateFromJsonFile<T>(
                CreateTestFileInfo(
                    nameOfTestFile, 
                    CreateTestFilesDirectoryInfo(testFileDirectory)));
        }

        public static T CreateFromJsonFile<T>(
            FileInfo testFileInfo,
            Encoding encoding = null) where T : class
        {
            var jsonString = GetFileContent(testFileInfo, encoding);
            if (jsonString.HasValue())
            {
                return jsonString.FromJsonString<T>();
            }

            return null;
        }

        /// <summary>
        /// Tries to load all the content from a file and return it as a string.
        /// </summary>
        /// <param name="nameOfTestFile"></param>
        /// <param name="directory"></param>
        /// <param name="encoding">If no encoding is provided the default value of Encoding.UTF8 will be used.</param>
        /// <returns></returns>
        public static string GetFileContent(
            string nameOfTestFile, 
            string directory = "_TestFiles",
            Encoding encoding = null)
        {

            return GetFileContent(CreateTestFileInfo(nameOfTestFile, CreateTestFilesDirectoryInfo(directory)), encoding);
        }

        public static string GetFileContent(
            string nameOfTestFile,
            DirectoryInfo testFileDirectoryInfo,
            Encoding encoding = null)
        {
            return GetFileContent(CreateTestFileInfo(nameOfTestFile, testFileDirectoryInfo), encoding);
        }

        /// <summary>
        /// Tries to load all the content from a file and return it as a string.
        /// </summary>
        /// <param name="testFileInfo"></param>
        /// <param name="encoding">If no encoding is provided the default value of Encoding.UTF8 will be used.</param>
        /// <returns></returns>
        public static string GetFileContent(
            FileInfo testFileInfo,
            Encoding encoding = null)
        {
            if (testFileInfo == null) throw new ArgumentNullException(nameof(testFileInfo));
            if (!File.Exists(testFileInfo.FullName)) 
                throw new FileNotFoundException($"Unable to find test data file: {testFileInfo.FullName}", testFileInfo.Name);

            return File.ReadAllText(testFileInfo.FullName, encoding ?? Encoding.UTF8);
        }
    }
}