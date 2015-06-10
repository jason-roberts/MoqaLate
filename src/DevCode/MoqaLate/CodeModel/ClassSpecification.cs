using System.Collections.Generic;

namespace MoqaLate.CodeModel
{
    public class ClassSpecification
    {
        public ClassSpecification()
        {
            Usings = new List<string>();
            Properties = new List<Property>();
            Methods = new List<Method>();
            Events = new List<Event>();
            IsValid = true;
        }

        public bool IsValid { get; set; }

        public string ClassName { get; set; }

        public List<string> Usings { get; set; }

        public List<Property> Properties { get; set; }

        public List<Method> Methods { get; set; }

        public string OriginalInterfaceName { get; set; }

        public string OriginalInterfaceNamespace { get; set; }

        public List<Event> Events { get; set; }

        public string InterfaceGenericTypes { get; set; }

        public bool IsPublic { get; set; }
    }
}