using System;
using System.Collections.Generic;
using AutoMoq;
using FluentAssertions;
using MoqaLate.CodeModel;
using MoqaLate.InterfaceTextParsing;
using NUnit.Framework;

namespace MoqaLate.Tests.Unit
{
    [TestFixture]
    public class InterfaceParserTest
    {
        private List<string> _interfaceLines;
        private InterfaceLineTextLineTextParser _sut;
        private List<string> _genericInterfaceLines;
        private List<string> _privateInterfaceLines;

        [SetUp]
        public void Setup()
        {
            CreateTestInterfaceText();
            CreateGenericTestInterfaceText();
            CreatePrivateInterfaceText();

            var mocker = new AutoMoqer();

            _sut = mocker.Resolve<InterfaceLineTextLineTextParser>();
        }



        [Test]
        public void ShouldRecognisePublicInterfaces()
        {
            var output = _sut.GenerateClass(CreateSimpleInterface("public"));

            output.IsPublic.Should().Be(true);
            output.IsValid.Should().Be(true);
        }


        [Test]
        public void ShouldRecogniseInternalInterfaces()
        {
            var output = _sut.GenerateClass(CreateSimpleInterface("internal"));

            output.IsPublic.Should().Be(false);
            output.IsValid.Should().Be(true);
        }


        [Test]
        public void ShouldParseNamespace()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.OriginalInterfaceNamespace.Should().Be("Awesome.Namespace");
        }


        [Test]
        public void ShouldParseClassName()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.ClassName.Should().Be("TestInterfaceMoqaLate");            
        }


        [Test]
        public void ShouldParseGenericClassName()
        {
            var output = _sut.GenerateClass(_genericInterfaceLines);

            output.ClassName.Should().Be("TestInterfaceMoqaLate");
        }


        [Test]
        public void ShouldParseInterfaceGenericTypes()
        {
            var output = _sut.GenerateClass(_genericInterfaceLines);

            output.ClassName.Should().Be("TestInterfaceMoqaLate");
            output.InterfaceGenericTypes.Should().Be("<T,K>");
        }


        [Test]
        public void ShouldParseOriginalInterfaceName()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.OriginalInterfaceName.Should().Be("ITestInterface");
        }

        [Test]
        public void ShouldParseAllNamespaces()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.Usings.Should().Contain("System.Collections.Generic");
            output.Usings.Should().Contain("FluentAssertions");
            output.Usings.Should().Contain("NUnit.Framework");
        }


        #region "Properties"


        
        [Test]
        public void ShouldParseGetSetProperties()
        {        
            var output = _sut.GenerateClass(_interfaceLines);

            output.Properties[0].Name.Should().Be("PropGetSet");
            output.Properties[0].Type.Should().Be("string");
            output.Properties[0].Accessor.Should().Be(PropertyAccessor.GetAndSet);         
        }

        [Test]
        public void ShouldParseGetProperties()
        {           
            var output = _sut.GenerateClass(_interfaceLines);

            output.Properties[1].Name.Should().Be("PropGet");
            output.Properties[1].Type.Should().Be("List<string>");
            output.Properties[1].Accessor.Should().Be(PropertyAccessor.GetOny);
        }

        [Test]
        public void ShouldParseSetProperties()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.Properties[2].Name.Should().Be("PropSet");
            output.Properties[2].Type.Should().Be("int");
            output.Properties[2].Accessor.Should().Be(PropertyAccessor.SetOny);
        }

        #endregion



        #region "methods"


        [Test]
        public void ShouldParseVoidMethodWithNoParams()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.Methods[0].Name.Should().Be("DoStuff");
            output.Methods[0].Parameters.Should().BeEmpty(string.Empty);
            output.Methods[0].ReturnType.Should().Be("void");
        }


        [Test]
        public void ShouldParseStringMethodWithNoParams()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            
            output.Methods[1].Name.Should().Be("DoStuff2");
            output.Methods[1].Parameters.Should().BeEmpty(string.Empty);
            output.Methods[1].ReturnType.Should().Be("string");
        }


        [Test]
        public void ShouldParseMethodWithMultipleParams()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.Methods[2].Name.Should().Be("DoStuff3");

            output.Methods[2].Parameters[0].Type.Should().Be("Func<bool, int>");
            output.Methods[2].Parameters[0].Name.Should().Be("p1");

            output.Methods[2].Parameters[1].Type.Should().Be("string");
            output.Methods[2].Parameters[1].Name.Should().Be("p2");
           
            output.Methods[2].ReturnType.Should().Be("List<List<int>>");
        }


        [Test]
        public void ShouldParseGenericWithinGenericMethodWithMultipleParams()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.Methods[3].Name.Should().Be("DoStuff4");

            output.Methods[3].Parameters[0].Type.Should().Be("Func<bool, int>");
            output.Methods[3].Parameters[0].Name.Should().Be("p1");
            
            output.Methods[3].ReturnType.Should().Be("Func<bool, int>");
        }


        [Test]
        public void ShouldParseVoidWithComplexParams()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.Methods[4].Name.Should().Be("LetsDoIt");

            output.Methods[4].Parameters[0].Type.Should().Be("Func<bool, int>");
            output.Methods[4].Parameters[0].Name.Should().Be("p1");

            output.Methods[4].Parameters[1].Type.Should().Be("string");
            output.Methods[4].Parameters[1].Name.Should().Be("p2");
            
            output.Methods[4].ReturnType.Should().Be("void");           
        }







        [Test]
        public void ShouldParseGenericMethod()
        {
            Assert.Inconclusive("void DoIt<T>(T p1)...");
        }

        
        #endregion



        #region "Events"


        [Test]
        public void ShouldParseEvent()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.Events[0].Name.Should().Be("E1");
            output.Events[0].Type.Should().Be("EventHandler");            
        }

        [Test]
        public void ShouldParseEventWithActionType()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.Events[1].Name.Should().Be("E2");
            output.Events[1].Type.Should().Be("Action<int>");
        }


        [Test]
        public void ShouldParseEventWithComplexActionType()
        {
            var output = _sut.GenerateClass(_interfaceLines);

            output.Events[2].Name.Should().Be("E3");
            output.Events[2].Type.Should().Be("Action<int,string>");
        }

        #endregion




        private void CreateTestInterfaceText()
        {
            _interfaceLines = new List<string>();

            _interfaceLines.Add("using System.Collections.Generic;");
            _interfaceLines.Add("using FluentAssertions;");
            _interfaceLines.Add("using NUnit.Framework;");

            _interfaceLines.Add("namespace Awesome.Namespace");
            _interfaceLines.Add("{");

            _interfaceLines.Add("public interface ITestInterface");
            _interfaceLines.Add("{");


                
    
            // some random attributes
            _interfaceLines.Add("[ComVisible(true)]");
            _interfaceLines.Add(@"   [Guid(""496B0ABE-CDEE-11d3-88E8-00902754C43A"")]");

            _interfaceLines.Add("   string PropGetSet { get; set; }");
            _interfaceLines.Add("   List<string> PropGet { get; }");
            _interfaceLines.Add("   int PropSet { set; }");

            _interfaceLines.Add("   void DoStuff();");
            _interfaceLines.Add("   string DoStuff2();");
            _interfaceLines.Add("   List<List<int>> DoStuff3(Func<bool, int> p1, string p2);");
            _interfaceLines.Add("   Func<bool, int> DoStuff4(Func<bool, int> p1);");
            _interfaceLines.Add("   void LetsDoIt(Func<bool, int> p1, string p2);");
            _interfaceLines.Add("   void LetsDoIt2(int p1 = 1, string p2 = null)");

            _interfaceLines.Add("   event EventHandler E1;");
            _interfaceLines.Add("   event Action<int> E2;");
            _interfaceLines.Add("   event Action<int,string> E3;");
            
            


            _interfaceLines.Add(@"   // this is a comment and should be ignored");



            _interfaceLines.Add("}"); // class

            _interfaceLines.Add("}"); // namespace
        }



        private void CreateGenericTestInterfaceText()
        {
            _genericInterfaceLines = new List<string>();


            _genericInterfaceLines.Add("namespace Awesome.Namespace");
            _genericInterfaceLines.Add("{");

            _genericInterfaceLines.Add("public interface ITestInterface<T,K>");
            _genericInterfaceLines.Add("{");

            _genericInterfaceLines.Add("   T PropGetSet { get; set; }");
            _genericInterfaceLines.Add("   K PropGet { get; }");            


            _genericInterfaceLines.Add("}"); // class

            _genericInterfaceLines.Add("}"); // namespace
        }


        private void CreatePrivateInterfaceText()
        {
            _privateInterfaceLines = new List<string>();


            _privateInterfaceLines.Add("namespace Awesome.Namespace");
            _privateInterfaceLines.Add("{");

            _privateInterfaceLines.Add("private interface ITestInterface<T,K>");
            _privateInterfaceLines.Add("{");

            _privateInterfaceLines.Add("   T PropGetSet { get; set; }");
            _privateInterfaceLines.Add("   K PropGet { get; }");


            _privateInterfaceLines.Add("}"); // class

            _privateInterfaceLines.Add("}"); // namespace
        }


        private List<string> CreateSimpleInterface(string accessModifier)
        {
            var lines = new List<string>();

            lines.Add("namespace Awesome.Namespace");
            lines.Add("{");

            lines.Add(accessModifier + " interface ITestInterface<T,K>");
            lines.Add("{");

            lines.Add("   T PropGetSet { get; set; }");
            lines.Add("   K PropGet { get; }");


            lines.Add("}"); // class

            lines.Add("}"); // namespace   

            return lines;
        }

    }


    // TODO: delegate ??    

}
