using System;
using System.IO;
using System.Linq;
using MoqaLate.Common;
using MoqaLate.InterfaceTextParsing;
using MoqaLate.IO;
using MoqaLate.MockClassBuilding;
using NUnit.Framework;
using FluentAssertions;

namespace MoqaLate.Tests.Integration
{
    [TestFixture]
    public class EngineTests
    {
        private string projectDir;
        private string sampleFilesInputPathRoot;

        [Test]
        public void ShouldWriteTestFiles()
        {
            var thisAssemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(this.GetType()).Location);

            projectDir = new DirectoryInfo(thisAssemblyPath).Parent.Parent.FullName;

            sampleFilesInputPathRoot = Path.Combine(projectDir, @"Integration\SampleFiles\a");

            var sut = new Engine(new CodeFileSearcher(new ConsoleLogger(), new IgnoredFilesProvider()), new FileContentLoader(new ConsoleLogger()), new InterfaceLineTextLineTextParser(new ConsoleLogger()),
                                 new ClassTextBuilder(new ConsoleLogger()), new FileWriter(new ConsoleLogger()), new ConsoleLogger());

            var outputDir = Path.Combine(projectDir, "GeneratedOutput");

            Array.ForEach(Directory.GetFiles(outputDir), path => File.Delete(path));

            sut.Process(sampleFilesInputPathRoot, outputDir);

            Directory.EnumerateFiles(outputDir).Count().Should().Be(5);
        }
    }
}
