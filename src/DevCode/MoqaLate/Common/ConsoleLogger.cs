using System;

namespace MoqaLate.Common
{
    public class ConsoleLogger :ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
