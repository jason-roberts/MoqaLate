using System;
using System.Collections.Generic;
using AutoMoq;
using FluentAssertions;
using MoqaLate.InterfaceTextParsing;
using NUnit.Framework;

namespace MoqaLate.Tests.Unit
{
    [TestFixture]
    public class MethodParametersParsersTest
    {
        [Test]
        public void ShouldParseSingle()
        {
            var paras = MethodParameterParser.Parse("int p1");

            paras.Count.Should().Be(1);

            paras[0].Type.Should().Be("int");
            paras[0].Name.Should().Be("p1");
        }


        [Test]
        public void ShouldParseMultiple()
        {
            var paras = MethodParameterParser.Parse("int p1, string p2");

            paras.Count.Should().Be(2);

            paras[0].Type.Should().Be("int");
            paras[0].Name.Should().Be("p1");

            paras[1].Type.Should().Be("string");
            paras[1].Name.Should().Be("p2");
        }



        [Test]
        public void ShouldParseDoubleComplexGeneric()
        {
            var paras = MethodParameterParser.Parse("Dictionary<List<int>, Dictionary<string, object>> p1, Dictionary<List<string>, Dictionary<float, int>> p2");

            paras.Count.Should().Be(2);

            paras[0].Type.Should().Be("Dictionary<List<int>, Dictionary<string, object>>");
            paras[0].Name.Should().Be("p1");

            paras[1].Type.Should().Be("Dictionary<List<string>, Dictionary<float, int>>");
            paras[1].Name.Should().Be("p2");
        }



        [Test]
        public void ShouldParseTrippleComplexGeneric()
        {
            var paras = MethodParameterParser.Parse("Dictionary<List<int>, Dictionary<string, object>> p1, Dictionary<List<string>, Dictionary<float, int>> p2, Dictionary<List<float>, Dictionary<float, int>> p3");

            paras.Count.Should().Be(3);

            paras[0].Type.Should().Be("Dictionary<List<int>, Dictionary<string, object>>");
            paras[0].Name.Should().Be("p1");

            paras[1].Type.Should().Be("Dictionary<List<string>, Dictionary<float, int>>");
            paras[1].Name.Should().Be("p2");

            paras[2].Type.Should().Be("Dictionary<List<float>, Dictionary<float, int>>");
            paras[2].Name.Should().Be("p3");
        }



        [Test]
        public void ShouldParseMixOfNonGenericAndComplexGenericWhenNonGenericFirst()
        {
            var paras = MethodParameterParser.Parse("int p1, Dictionary<List<string>, Dictionary<float, int>> p2");

            paras.Count.Should().Be(2);

            paras[0].Type.Should().Be("int");
            paras[0].Name.Should().Be("p1");

            paras[1].Type.Should().Be("Dictionary<List<string>, Dictionary<float, int>>");
            paras[1].Name.Should().Be("p2");
        }


        [Test]
        public void ShouldParseMixOfNonGenericAndComplexGenericWhenNonGenericSecond()
        {
            var paras = MethodParameterParser.Parse("Dictionary<List<string>, Dictionary<float, int>> p1, int p2");

            paras.Count.Should().Be(2);

            paras[0].Type.Should().Be("Dictionary<List<string>, Dictionary<float, int>>");
            paras[0].Name.Should().Be("p1");

            paras[1].Type.Should().Be("int");
            paras[1].Name.Should().Be("p2");
        }



        [Test]
        public void ShouldParseMixOfNonGenericAndComplexGenericWhenNonGenericMiddle()
        {
            var paras = MethodParameterParser.Parse("Dictionary<List<int>, Dictionary<string, object>> p1, int p2, Dictionary<List<float>, Dictionary<float, int>> p3");

            paras.Count.Should().Be(3);

            paras[0].Type.Should().Be("Dictionary<List<int>, Dictionary<string, object>>");
            paras[0].Name.Should().Be("p1");

            paras[1].Type.Should().Be("int");
            paras[1].Name.Should().Be("p2");

            paras[2].Type.Should().Be("Dictionary<List<float>, Dictionary<float, int>>");
            paras[2].Name.Should().Be("p3");
        }



        

    }
}