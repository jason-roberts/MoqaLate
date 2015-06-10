using System.Collections.Generic;

namespace MoqaLate.IO
{
    public interface IIgnoredFilesProvider
    {
        IEnumerable<string> GetIgnoredFiles();
    }
}