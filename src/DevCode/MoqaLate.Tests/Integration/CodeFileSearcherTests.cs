using System.Collections.Generic;
using System.IO;
using AutoMoq;
using FluentAssertions;
using MoqaLate.IO;
using NUnit.Framework;

namespace MoqaLate.Tests.Integration
{
    [TestFixture]
    public class CodeFileSearcherTests
    {


        [SetUp]
        public void Setup()
        {
            var thisAssemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(GetType()).Location);

            solutionDir = new DirectoryInfo(thisAssemblyPath).Parent.Parent.FullName;

            sampleFilesPath = Path.Combine(solutionDir, @"Integration\SampleFiles\a");
            sampleFilesPathNested1 = sampleFilesPath + @"\nested1";

            _autoMoqer = new AutoMoqer();

            sut = _autoMoqer.Resolve<CodeFileSearcher>();

            _autoMoqer.GetMock<IIgnoredFilesProvider>().Setup(x => x.GetIgnoredFiles()).Returns(new List<string>
                                                                                                    {
                                                                                                        "xaml.cs",
                                                                                                        "assemblyinfo.cs"
                                                                                                    });
        }



        private string solutionDir;
        private string sampleFilesPath;
        private string sampleFilesPathNested1;

        private ICodeFileSearcher sut;
        private AutoMoqer _autoMoqer;


        [Test]
        public void ShouldBeCaseInsensitiveSearch()
        {
            var codeFiles = sut.SearchForCodeFiles(sampleFilesPath);

            codeFiles[0].Should().Be(Path.Combine(sampleFilesPath, "IClass1.cs"));
            codeFiles[1].Should().Be(Path.Combine(sampleFilesPath, "IClass2.cs"));
            codeFiles[2].Should().Be(Path.Combine(sampleFilesPath, "IClass3.cs"));
            codeFiles[3].Should().Be(Path.Combine(sampleFilesPath, "IClass4.CS"));
        }

        [Test]
        public void ShouldExclueNonCSharpFilesGetListOfCodeFiles()
        {
            var codeFiles = sut.SearchForCodeFiles(sampleFilesPath);

            codeFiles.Count.Should().Be(6);
        }

        [Test]
        public void ShouldGetListOfCodeFiles()
        {
            var codeFiles = sut.SearchForCodeFiles(sampleFilesPath);

            codeFiles[0].Should().Be(Path.Combine(sampleFilesPath, "IClass1.cs"));
            codeFiles[1].Should().Be(Path.Combine(sampleFilesPath, "IClass2.cs"));
            codeFiles[2].Should().Be(Path.Combine(sampleFilesPath, "IClass3.cs"));
            codeFiles[3].Should().Be(Path.Combine(sampleFilesPath, "IClass4.CS"));
        }


        [Test]
        public void ShouldGetNestedSubDirFiles()
        {
            var codeFiles = sut.SearchForCodeFiles(sampleFilesPath);

            codeFiles[0].Should().Be(Path.Combine(sampleFilesPath, "IClass1.cs"));
            codeFiles[1].Should().Be(Path.Combine(sampleFilesPath, "IClass2.cs"));
            codeFiles[2].Should().Be(Path.Combine(sampleFilesPath, "IClass3.cs"));
            codeFiles[3].Should().Be(Path.Combine(sampleFilesPath, "IClass4.CS"));
            codeFiles[4].Should().Be(Path.Combine(sampleFilesPath, "IClass6.cs"));
            codeFiles[5].Should().Be(Path.Combine(sampleFilesPathNested1, "IClass5.cs"));
        }



    }
}