using System.Diagnostics;
using System.IO;
using MoqaLate.Common;
using MoqaLate.InterfaceTextParsing;
using MoqaLate.IO;
using MoqaLate.MockClassBuilding;

namespace MoqaLate
{
    public class Engine
    {
        private readonly IClassTextBuilder _builder;
        private readonly IFileContentLoader _loader;
        private readonly ILogger _logger;
        private readonly IInterfaceLineTextParser _lineTextParser;
        private readonly ICodeFileSearcher _searcher;
        private readonly IFileWriter _writer;

        public Engine(ICodeFileSearcher searcher, IFileContentLoader loader, IInterfaceLineTextParser lineTextParser,
                      IClassTextBuilder builder, IFileWriter writer, ILogger logger)
        {
            _searcher = searcher;
            _loader = loader;
            _lineTextParser = lineTextParser;
            _builder = builder;
            _writer = writer;
            _logger = logger;
        }

        public void Process(string sourceDir, string destDir)
        {
            var fileNames = _searcher.SearchForCodeFiles(sourceDir);

            foreach (var fileName in fileNames)
            {
                Debug.WriteLine(fileName);
                var codeLines = _loader.LoadFilesLines(fileName);

                var mockClassSpec = _lineTextParser.GenerateClass(codeLines);

                if (mockClassSpec.IsValid && mockClassSpec.IsPublic)
                {
                    var mockClassText = _builder.Create(mockClassSpec);

                    if (!Directory.Exists(destDir))
                        Directory.CreateDirectory(destDir);

                    var outputFilePath = Path.Combine(destDir + @"\" + mockClassSpec.ClassName + ".cs");

                    _writer.Write(mockClassText, outputFilePath);
                }
                else
                {
                    _logger.Write(string.Format("File '{0}' is not an interface and will not be processed.", fileName));
                }
            }
        }
    }
}