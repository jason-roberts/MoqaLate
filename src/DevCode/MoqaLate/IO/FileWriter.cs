using System.IO;
using MoqaLate.Common;

namespace MoqaLate.IO
{
    public class FileWriter : IFileWriter
    {
        private readonly ILogger _logger;

        public FileWriter(ILogger logger)
        {
            _logger = logger;
        }

        #region IFileWriter Members

        public void Write(string classText, string destDir)
        {
            _logger.Write(string.Format("Writing file '{0}'", destDir));

            File.WriteAllText(destDir, classText);
        }

        #endregion
    }
}