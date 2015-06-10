using System.Linq;
using AutoMoq;
using FluentAssertions;
using MoqaLate.IO;
using NUnit.Framework;

namespace MoqaLate.Tests.Integration
{
    [TestFixture]
    public class IgnoreFilesProviderTests
    {
        [Test]
        public void ShouldReadExcludedFiles()
        {
            var autoMoqer = new AutoMoqer();

            var sut = autoMoqer.Resolve<IgnoredFilesProvider>();

            var excludedFiles = sut.GetIgnoredFiles().ToList();

            excludedFiles[0].Should().Be("xaml.cs");
            excludedFiles[1].Should().Be("assemblyinfo.cs");
        }
    }
}