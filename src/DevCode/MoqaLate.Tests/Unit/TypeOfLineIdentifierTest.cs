using FluentAssertions;
using MoqaLate.InterfaceTextParsing;
using NUnit.Framework;

namespace MoqaLate.Tests.Unit
{
    [TestFixture]
    public class TypeOfLineIdentifierTest
    {
        [Test]
        public void ShouldRecogniseCloseBrace()
        {
            TypeOfLineIdentifier.Identify("  }  ").Should().Be(InterfaceDefinitionLineType.Brace);
        }

        [Test]
        public void ShouldRecogniseEmptyLine()
        {
            TypeOfLineIdentifier.Identify("    ").Should().Be(InterfaceDefinitionLineType.EmptyLine);
        }


        [Test]
        public void ShouldRecogniseEvent()
        {
            TypeOfLineIdentifier.Identify("  event xxx;  ").Should().Be(InterfaceDefinitionLineType.Event);
        }

        [Test]
        public void ShouldNotRecogniseEventWhenTheWordEventIsUsedElsewhere()
        {
            TypeOfLineIdentifier.Identify("  void Processevent();  ").Should().Be(InterfaceDefinitionLineType.Method);
        }

        [Test]
        public void ShouldRecogniseGetterPropertyLine()
        {
            TypeOfLineIdentifier.Identify("     { get;  }   ").Should().Be(InterfaceDefinitionLineType.PropertyGet);
        }

        [Test]
        public void ShouldRecogniseGetterSetterPropertyLine()
        {
            TypeOfLineIdentifier.Identify("     { get; set; }   ").Should().Be(
                InterfaceDefinitionLineType.PropertyGetSet);
        }

        [Test]
        public void ShouldRecogniseInterfaceName()
        {
            TypeOfLineIdentifier.Identify("     interface    ").Should().Be(InterfaceDefinitionLineType.Interface);
        }


        [Test]
        public void ShouldRecogniseMethod()
        {
            TypeOfLineIdentifier.Identify("  void DoIt()  ").Should().Be(InterfaceDefinitionLineType.Method);
        }

        [Test]
        public void ShouldRecogniseNamespace()
        {
            TypeOfLineIdentifier.Identify("    namespace Xxx.Yyy.Zzz   ").Should().Be(
                InterfaceDefinitionLineType.Namespace);
        }


        [Test]
        public void ShouldNotRecogniseNamespaceWhenTheWordNamespaceIsUsedElsewhere()
        {
            TypeOfLineIdentifier.Identify("  void  Processnamespace();").Should().Be(
                InterfaceDefinitionLineType.Method);
        }

        [Test]
        public void ShouldRecogniseOpenBrace()
        {
            TypeOfLineIdentifier.Identify("  {  ").Should().Be(InterfaceDefinitionLineType.Brace);
        }

        [Test]
        public void ShouldRecogniseSetterPropertyLine()
        {
            TypeOfLineIdentifier.Identify("     { set;  }   ").Should().Be(InterfaceDefinitionLineType.PropertySet);
        }

        [Test]
        public void ShouldRecogniseUsing()
        {
            TypeOfLineIdentifier.Identify("     using    ").Should().Be(InterfaceDefinitionLineType.Using);
        }

        // TODO: somewhere need to ignore all non public interfaces as we wont be able to reference them in the test project where we'll gen the mocks
    }
}