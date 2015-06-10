using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoqaLate.Common;

namespace MoqaLate.IO
{
    public class CodeFileSearcher : ICodeFileSearcher
    {
        private readonly IIgnoredFilesProvider _ignoredFilesProvider;
        private readonly ILogger _logger;

        public CodeFileSearcher(ILogger logger, IIgnoredFilesProvider ignoredFilesProvider)
        {
            _logger = logger;
            _ignoredFilesProvider = ignoredFilesProvider;
        }

        #region ICodeFileSearcher Members

        public List<string> SearchForCodeFiles(string rootPath)
        {
            const string fileSpec = "*.cs";

            _logger.Write(string.Format("Searching '{0}' and sub directories for all '{1}' files", rootPath, fileSpec));

            var allCsFiles = Directory.EnumerateFiles(rootPath, fileSpec, SearchOption.AllDirectories).ToList();

            return FilteredList(allCsFiles);
        }

        #endregion

        private List<string> FilteredList(List<string> allCsFiles)
        {
            var filesToIgnore = _ignoredFilesProvider.GetIgnoredFiles();

            if (filesToIgnore.Count() == 0)
                return allCsFiles;

            var includedFiles = new List<string>();

            foreach (var fileName in allCsFiles)
            {
                var exclude = false;

                foreach (var excludedFile in filesToIgnore)
                {
                    if (fileName.ToLower().Contains(excludedFile.ToLower()))
                        exclude = true;
                }

                if (!exclude)
                    includedFiles.Add(fileName);
            }

            return includedFiles.ToList();
        }
    }
}