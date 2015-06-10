using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoqaLate.InterfaceTextParsing
{
    public enum InterfaceDefinitionLineType
    {
        Namespace,
        Interface,
        Using,
        PropertyGetSet,
        PropertyGet,
        PropertySet,
        EmptyLine,
        Brace,
        Event,
        Method
    }
}
