using System.Collections.Generic;
using System.Linq;

namespace MoqaLate.InterfaceTextParsing
{
    public static class CommentLineRemover
    {
        public static List<string> Remove(List<string> linesWithComments)
        {
            return linesWithComments.Where(line => !line.Trim().StartsWith(@"//")).ToList();
        }
    }
}
