using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoqaLate.Common;

namespace MoqaLate.IO
{
    public class FileContentLoader : IFileContentLoader
    {
        private readonly ILogger _logger;

        public FileContentLoader(ILogger logger)
        {
            _logger = logger;
        }

        #region IFileContentLoader Members

        public List<string> LoadFilesLines(string filePath)
        {
            _logger.Write(string.Format("Loading file '{0}'", filePath));

            return File.ReadAllLines(filePath).ToList<string>();
        }

        #endregion
    }
}