using System.Collections.Generic;

namespace MoqaLate.IO
{
    public interface IFileContentLoader
    {
        List<string> LoadFilesLines(string filePath);
    }
}