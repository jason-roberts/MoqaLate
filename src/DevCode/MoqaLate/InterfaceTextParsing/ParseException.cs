using System;

namespace MoqaLate.InterfaceTextParsing
{
    public class ParseException : ApplicationException
    {
        public ParseException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}