using System.Collections.Generic;

namespace MoqaLate.IO
{
    public interface ICodeFileSearcher
    {
        List<string> SearchForCodeFiles(string rootPath);
    }
}