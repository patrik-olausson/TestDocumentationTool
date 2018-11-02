using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using TestDocumentationTool.Api;
using Xunit;

namespace AssemblyLoaderThatRequiresValidDirectoryTests
{
    [ExcludeFromCodeCoverage]
    public class LoadAssemblies : AssemblyLoaderThatRequiresValidDirectoryTestharness
    {
        [Fact]
        public void GivenADirectoryInfoThatIsNull_ThenArgumentNullExceptionIsThrown()
        {
            DirectoryInfo directoryInfo = null;
            var sut = CreateSut();

            Assert.Throws<ArgumentNullException>(() => sut.LoadAssemblies(directoryInfo));
        }

        [Fact]
        public void GivenADirectoryInfoThatHasAnInvalidPath_ThenArgumentExceptionIsThrown()
        {
            var directoryInfo = new DirectoryInfo(@"NotLikely\To\Be\A\ValidPath");
            var sut = CreateSut();

            Assert.Throws<ArgumentException>(() => sut.LoadAssemblies(directoryInfo));
        }
    }

    [ExcludeFromCodeCoverage]
    public class AssemblyLoaderThatRequiresValidDirectoryTestharness
    {
        protected AssemblyLoaderThatRequiresValidDirectory CreateSut()
        {
            return new AssemblyLoaderThatRequiresValidDirectory();
        }
    }
}
