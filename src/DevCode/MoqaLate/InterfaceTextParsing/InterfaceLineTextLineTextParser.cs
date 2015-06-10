using System;
using System.Collections.Generic;
using System.Linq;
using MoqaLate.CodeModel;
using MoqaLate.Common;
using MoqaLate.ExtensionMethods;

namespace MoqaLate.InterfaceTextParsing
{
    public class InterfaceLineTextLineTextParser : IInterfaceLineTextParser
    {
        private const string UsingSymbol = "using ";
        private const string InterfaceSymbol = "interface ";
        private readonly ILogger _logger;

        private ClassSpecification _classSpec;
        private List<string> _linesOfInterfaceCode;

        public InterfaceLineTextLineTextParser(ILogger logger)
        {
            _logger = logger;
        }

        #region IInterfaceLineTextParser Members

        public ClassSpecification GenerateClass(List<string> linesOfInterfaceCode)
        {
            _classSpec = new ClassSpecification();

            _linesOfInterfaceCode = linesOfInterfaceCode;

            _linesOfInterfaceCode = CommentLineRemover.Remove(_linesOfInterfaceCode);

            _linesOfInterfaceCode = RemoveAttributes(_linesOfInterfaceCode);

            _classSpec = new ClassSpecification();






            ParseNamespace();

            ParseClassName();

            if (_classSpec.ClassName == null)
            {
                _classSpec.IsValid = false;
                return _classSpec;
            }

            ParseUsings();

            ParseProperties();

            ParseMethods();

            ParseEvents();

            return _classSpec;
        }

        #endregion

        private void ParseEvents()
        {
            _logger.Write("Parsing events");

            var lineNum = 0;

            foreach (var line in _linesOfInterfaceCode)
            {
                try
                {
                    lineNum++;

                    if (TypeOfLineIdentifier.Identify(line) == InterfaceDefinitionLineType.Event)
                    {
                        ParseEventLine(line);
                    }
                }
                catch (Exception ex)
                {
                    throw new ParseException(
                        string.Format("Error parsing surrounding event in file '{0}' at line {1}", _classSpec.ClassName,
                                      lineNum), ex);
                }
            }
        }

        private void ParseEventLine(string line)
        {
            var trimmedLine = line.Trim();

            var eventType = trimmedLine.Replace("event ", "").Split(new char[] {' '})[0];

            var eventName =
                trimmedLine.Substring(trimmedLine.PositionOfSpaceBefore(trimmedLine.Length - 1)).TrimEnd(new[] {';'}).
                    Trim();

            _classSpec.Events.Add(new Event {Name = eventName, Type = eventType});
        }


        private void ParseNamespace()
        {
            _logger.Write("Parsing containing namespace");

            var lineNum = 0;

            foreach (var line in _linesOfInterfaceCode)
            {
                try
                {
                    lineNum++;

                    if (TypeOfLineIdentifier.Identify(line) == InterfaceDefinitionLineType.Namespace)
                    {
                        ParseNameSpaceLine(line);
                    }
                }
                catch (Exception ex)
                {
                    throw new ParseException(
                        string.Format("Error parsing surrounding namespace in file '{0}' at line {1}",
                                      _classSpec.ClassName,
                                      lineNum), ex);
                }
            }
        }

        private void ParseNameSpaceLine(string line)
        {
            _classSpec.OriginalInterfaceNamespace = line.Trim().Split(new[] {' '})[1];
        }


        private List<string> RemoveAttributes(List<string> linesOfInterfaceCode)
        {
            return linesOfInterfaceCode.Where(line => ! line.Trim().StartsWith("[")).ToList();
        }


        private void ParseProperties()
        {
            _logger.Write("Parsing properties");

            var lineNum = 0;

            foreach (var line in _linesOfInterfaceCode)
            {
                try
                {
                    lineNum++;

                    if (TypeOfLineIdentifier.Identify(line) == InterfaceDefinitionLineType.PropertyGetSet)
                    {
                        ParseGetterSetterPropertyLine(line);
                    }

                    if (TypeOfLineIdentifier.Identify(line) == InterfaceDefinitionLineType.PropertyGet)
                    {
                        ParseGetterOnlyPropertyLine(line);
                    }


                    if (TypeOfLineIdentifier.Identify(line) ==InterfaceDefinitionLineType.PropertySet)
                    {
                        ParseSetterOnlyPropertyLine(line);
                    }
                }
                catch (Exception ex)
                {
                    throw new ParseException(
                        string.Format("Error parsing a property in file '{0}' at line {1}", _classSpec.ClassName,
                                      lineNum), ex);
                }
            }
        }

        private void ParseSetterOnlyPropertyLine(string line)
        {
            var prop = new Property
                           {
                               Type = line.Trim().Split(new[] {' '})[0],
                               Accessor = PropertyAccessor.SetOny,
                               Name = line.Trim().Split(new[] {' '})[1]
                           };

            _classSpec.Properties.Add(prop);
        }

        private void ParseGetterOnlyPropertyLine(string line)
        {
            var prop = new Property
                           {
                               Type = line.Trim().Split(new[] {' '})[0],
                               Accessor = PropertyAccessor.GetOny,
                               Name = line.Trim().Split(new[] {' '})[1]
                           };

            _classSpec.Properties.Add(prop);
        }

        private void ParseGetterSetterPropertyLine(string line)
        {
            var prop = new Property
                           {
                               Type = line.Trim().Split(new[] {' '})[0],
                               Accessor = PropertyAccessor.GetAndSet,
                               Name = line.Trim().Split(new[] {' '})[1]
                           };

            _classSpec.Properties.Add(prop);
        }


        private void ParseUsings()
        {
            _logger.Write("Parsing using statements");

            var lineNum = 0;

            foreach (var line in _linesOfInterfaceCode)
            {
                try
                {
                    lineNum++;

                    if (TypeOfLineIdentifier.Identify(line) == InterfaceDefinitionLineType.Using)
                    {
                        ParseUsingLine(line);
                    }
                }
                catch (Exception ex)
                {
                    throw new ParseException(
                        string.Format("Error parsing a using in file '{0}' at line {1}", _classSpec.ClassName,
                                      lineNum), ex);
                }
            }
        }

        private void ParseUsingLine(string line)
        {
            var namespaceNamePosition = line.IndexOf(UsingSymbol) + UsingSymbol.Length;

            var nameSpace = line.Substring(namespaceNamePosition);


            _classSpec.Usings.Add(nameSpace.Replace(";", ""));
        }


        private void ParseClassName()
        {
            _logger.Write("Parsing class name (looking for 'interface' text)");


            foreach (var line in _linesOfInterfaceCode)
            {
                if (TypeOfLineIdentifier.Identify(line) == InterfaceDefinitionLineType.Interface)
                {
                    ParseInterfaceDeclarationLine(line);
                    return;
                }
            }
        }

        private void ParseInterfaceDeclarationLine(string line)
        {
            if (! line.Trim().StartsWith(InterfaceSymbol))
            {
                var accessModifer = line.Trim().Split(new[] {' '})[0];

                _classSpec.IsPublic = accessModifer == "public";
            }

            var interfaceNamePosition = line.IndexOf(InterfaceSymbol) + InterfaceSymbol.Length;

            var interfaceName = line.Substring(interfaceNamePosition);

            if (interfaceName.Contains("<")) // look for generic types
            {
                var startPositionGeneicDeclarations = interfaceName.IndexOf("<");

                _classSpec.InterfaceGenericTypes = interfaceName.Substring(startPositionGeneicDeclarations);

                interfaceName = interfaceName.Remove(startPositionGeneicDeclarations);
            }

            _classSpec.ClassName = interfaceName.Substring(1) + "MoqaLate";

            _classSpec.OriginalInterfaceName = interfaceName;
        }


        private void ParseMethods()
        {
            _logger.Write("Parsing methods");

            foreach (var line in _linesOfInterfaceCode)
            {
                if (
                    TypeOfLineIdentifier.Identify(line) == InterfaceDefinitionLineType.Method
                    )
                {
                    ParseMethodLine(line);
                }
            }
        }

        private void ParseMethodLine(string line)
        {
            var trimmdLine = line.Trim();

            var posOfStartOfMethodName = trimmdLine.PositionOfSpaceBefore('(');

            var returnType = trimmdLine.Substring(0, posOfStartOfMethodName);

            var nameAndParameters = trimmdLine.Substring(posOfStartOfMethodName + 1);

            var name = nameAndParameters.Substring(0, nameAndParameters.IndexOf('('));

            var indexOfOpenParen = nameAndParameters.IndexOf('(');
            var indexOfCloseParen = nameAndParameters.IndexOf(')');


            var parameters = nameAndParameters.Substring(indexOfOpenParen + 1, indexOfCloseParen - indexOfOpenParen - 1);

            _classSpec.Methods.Add(new Method
                                       {
                                           Name = name,
                                           ReturnType = returnType,
                                           Parameters = MethodParameterParser.Parse(parameters)
                                       });
        }


    }
}