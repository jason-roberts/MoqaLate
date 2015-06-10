using FluentAssertions;
using System.Collections.Generic;
using MoqaLate.InterfaceTextParsing;
using NUnit.Framework;

namespace MoqaLate.Tests.Unit
{
    [TestFixture]
    public class CommentLineRemoverTests
    {
        [Test]
        public void ShouldRemoveSingleLineComments()
        {
            var linesWithComments = new List<string>
                                        {
                                            "line1",
                                            "// comment 1",
                                            "line2 // xxx",
                                            "// comment 2",
                                            "line3"
                                        };

            var output = CommentLineRemover.Remove(linesWithComments);

            output[0].Should().Be("line1");
            output[1].Should().Be("line2 // xxx");
            output[2].Should().Be("line3");

            output.Count.Should().Be(3);
        }


        [Test]
        public void ShouldRemoveDoxSmlComments()
        {
            var linesWithComments = new List<string>
                                        {
                                            "line1",
                                            "/// <summary>",
                                            "/// stuff",
                                            "/// <summary>",
                                            "line2",
                                            "line3"
                                        };

            var output = CommentLineRemover.Remove(linesWithComments);

            output[0].Should().Be("line1");
            output[1].Should().Be("line2");
            output[2].Should().Be("line3");

            output.Count.Should().Be(3);
        }
    }
}