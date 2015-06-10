using System;
using System.Collections.Generic;
using MoqaLate.CodeModel;
using MoqaLate.ExtensionMethods;

namespace MoqaLate.InterfaceTextParsing
{
    public class MethodParameterParser
    {
        public static MethodParameterList Parse(string parameters)
        {
            return parameters.Contains("<") ? ParseGeneric(parameters) : ParseNonGeneric(parameters);
        }


        private static MethodParameterList ParseNonGeneric(string parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters))
                return new MethodParameterList();

            var paramDefinition = parameters.Trim().Split(new[] { ',' });

            var methodParameters = new MethodParameterList();

            Array.ForEach(paramDefinition, paramString =>
            {
                var defComponents = paramString.Trim().Split(new[] { ' ' });
                methodParameters.Add(new MethodParameter { Type = defComponents[0], Name = defComponents[1] });
            });


            return methodParameters;
        }

        private static MethodParameterList ParseGeneric(string parametersContaingGenerics)
        {
            if (! parametersContaingGenerics.Contains("<"))
                throw new ArgumentException(string.Format("'{0}' does not contain generic parameters.",
                                                          parametersContaingGenerics));

            var methodParameters = new MethodParameterList();

            var paramSeparatorPositions = new List<int>();

            var trimmed = parametersContaingGenerics.Trim();

            var angleBracketStack = new Stack<object>();

            for (var i = 0; i < trimmed.Length; i++)
            {
                var currentChar = trimmed[i];

                if (currentChar == '<')
                    angleBracketStack.Push(null);

                if (currentChar == '>')
                    angleBracketStack.Pop();

                if (currentChar == ',' && angleBracketStack.Count == 0)
                {
                    paramSeparatorPositions.Add(i);
                }
            }


            var couplets = new List<string>();


            if (paramSeparatorPositions.Count == 0) // then only one to split
            {
                couplets.Add(parametersContaingGenerics);
                
            }
            else
            {

                for (int currentCoupletIndex = 0;
                     currentCoupletIndex < paramSeparatorPositions.Count+1;
                     currentCoupletIndex++)
                {
                    int startPos, endPos;

                    if (currentCoupletIndex == 0) // first one
                    {
                        startPos = 0;
                        endPos = paramSeparatorPositions[currentCoupletIndex];

                        couplets.Add(parametersContaingGenerics.Substring(startPos, endPos - startPos));
                    }
                    else if (currentCoupletIndex == paramSeparatorPositions.Count) // last one
                    {
                        startPos = paramSeparatorPositions[currentCoupletIndex-1] + 1;

                        couplets.Add(parametersContaingGenerics.Substring(startPos).Trim());
                
                    }
                    else // where are neither 1st or last couplet
                    {
                        startPos = paramSeparatorPositions[currentCoupletIndex-1]+1;
                        endPos = paramSeparatorPositions[currentCoupletIndex];

                        couplets.Add(parametersContaingGenerics.Substring(startPos, endPos - startPos).Trim());
                    }
                }
            }


            
            foreach (var couplet in couplets)
            {
                methodParameters.Add(ParseCouplet(couplet));    
            }


            return methodParameters;
        }

        private static MethodParameter ParseCouplet(string paramCouplet)
        {
            var spaceSeparatorPos = paramCouplet.PositionOfSpaceBefore(paramCouplet.Length-1);

            return new MethodParameter {Name = paramCouplet.Substring(spaceSeparatorPos).Trim(), Type = paramCouplet.Substring(0, spaceSeparatorPos)};
        }
    }
}
