using System.Diagnostics.CodeAnalysis;
using System.IO;
using TestDocumentationTool.Api;
using ToolboxForTesting;
using Xunit;

namespace AssemblyLoaderThatAlwaysReturnSomethingTests
{
    [ExcludeFromCodeCoverage]
    public class LoadAssemblies : AssemblyLoaderThatAlwaysReturnSomethingTestharness
    {
        [Fact]
        public void GivenADirectoryInfoThatIsNull_ThenAnEmptyListIsReturned()
        {
            var sut = CreateSut();

            var result = sut.LoadAssemblies((DirectoryInfo) null);

            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void GivenADirectoryInfoThatHasAnInvalidPath_ThenAnEmptyListIsReturned()
        {
            var directoryInfo = new DirectoryInfo(@"NotLikely\To\Be\A\ValidPath");
            var sut = CreateSut();

            var result = sut.LoadAssemblies(directoryInfo);

            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void GivenADirectoryInfoThatPointsToAnEmptyDirectory_ThenAnEmptyListIsReturned()
        {
            var testDirectory = CreateTestDirectoryInfo("EmptyDirectory");
            var sut = CreateSut();

            var result = sut.LoadAssemblies(testDirectory);

            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void GivenADirectoryInfoThatPointsToDirectoryWithAssembliesWithoutAnyTests_ThenAnEmptyListIsReturned()
        {
            var testDirectory = CreateTestDirectoryInfo("DirectoryWithAssembliesWithoutTests");
            var sut = CreateSut();

            var result = sut.LoadAssemblies(testDirectory);

            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void GivenADirectoryInfoThatPointsToDirectoryWithTwoAssembliesThatContainsTests_ThenAListWithTwoEntriesIsReturned()
        {
            var testDirectory = CreateTestDirectoryInfo("DirectoryWithAssembliesContainingTests");
            var sut = CreateSut();

            var result = sut.LoadAssemblies(testDirectory);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GivenADirectoryInfoThatPointsToDirectoryWithTwoDuplicateAssembliesThatContainsTests_ThenAListWithTwoEntriesIsReturned()
        {
            var testDirectory = CreateTestDirectoryInfo("DirectoryWithDuplicateAssembliesContainingTests");
            var sut = CreateSut();

            var result = sut.LoadAssemblies(testDirectory);

            Assert.Equal(2, result.Count);
        }
    }

    [ExcludeFromCodeCoverage]
    public class AssemblyLoaderThatAlwaysReturnSomethingTestharness
    {
        protected AssemblyLoaderThatAlwaysReturnSomething CreateSut()
        {
            return new AssemblyLoaderThatAlwaysReturnSomething();
        }

        /// <summary>
        /// Creates a directory info for a directory name with the path relative to this
        /// test suite test file directory.
        /// </summary>
        protected DirectoryInfo CreateTestDirectoryInfo(string directoryName)
        {
            return FileHelper.CreateTestFilesDirectoryInfo($@"Reflection\AssemblyLoaderTests\_TestFiles\{directoryName}");
        }
    }
}
