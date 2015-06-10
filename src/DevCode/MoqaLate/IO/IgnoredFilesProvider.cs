using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MoqaLate.IO
{
    public class IgnoredFilesProvider : IIgnoredFilesProvider
    {
        #region IIgnoredFilesProvider Members

        public IEnumerable<string> GetIgnoredFiles()
        {
            var fileLines = ReadLineFromExcludesFile();

            return ParseFileLines(fileLines);
        }

        #endregion

        private IEnumerable<string> ParseFileLines(string[] fileLines)
        {
            return (from fileLine in fileLines
                    where !string.IsNullOrWhiteSpace(fileLine) && !fileLine.StartsWith("#")
                    select fileLine.Trim()).ToList();
        }

        private string[] ReadLineFromExcludesFile()
        {
            var thisAssemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(GetType()).Location);

            return File.ReadAllLines(thisAssemblyPath + @"\MoqaLateExcludedFiles.txt");
        }
    }
}