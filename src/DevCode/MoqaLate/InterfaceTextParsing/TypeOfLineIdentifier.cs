using MoqaLate.ExtensionMethods;

namespace MoqaLate.InterfaceTextParsing
{
    public static class TypeOfLineIdentifier
    {
        public static InterfaceDefinitionLineType Identify(string interfaceSourceCodeLine)
        {
            if (interfaceSourceCodeLine.CanonicalString().StartsWith(CSharpSymbol.Namespace.CanonicalString()))
                return InterfaceDefinitionLineType.Namespace;
            
            if (interfaceSourceCodeLine.CanonicalString().Contains(CSharpSymbol.Interface.CanonicalString()))
                return InterfaceDefinitionLineType.Interface;

            if (interfaceSourceCodeLine.CanonicalString().Contains(CSharpSymbol.PropertyGetSet.CanonicalString()))
                return InterfaceDefinitionLineType.PropertyGetSet;

            if (interfaceSourceCodeLine.CanonicalString().Contains(CSharpSymbol.PropertyGet.CanonicalString()))
                return InterfaceDefinitionLineType.PropertyGet;

            if (interfaceSourceCodeLine.CanonicalString().Contains(CSharpSymbol.PropertySet.CanonicalString()))
                return InterfaceDefinitionLineType.PropertySet;

            if (string.IsNullOrWhiteSpace(interfaceSourceCodeLine))
                return InterfaceDefinitionLineType.EmptyLine;

            if (interfaceSourceCodeLine.CanonicalString().Contains(CSharpSymbol.OpenBrace.CanonicalString()) ||
                interfaceSourceCodeLine.CanonicalString().Contains(CSharpSymbol.CloseBrace.CanonicalString()))
                return InterfaceDefinitionLineType.Brace;

            if (interfaceSourceCodeLine.CanonicalString().StartsWith(CSharpSymbol.Event.CanonicalString()))
                return InterfaceDefinitionLineType.Event;

            if (interfaceSourceCodeLine.CanonicalString().Contains(CSharpSymbol.Using.CanonicalString()))
                return InterfaceDefinitionLineType.Using;

            return InterfaceDefinitionLineType.Method;
        }
    }
}
