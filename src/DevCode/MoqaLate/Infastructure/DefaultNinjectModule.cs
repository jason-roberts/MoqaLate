using System;
using MoqaLate.Common;
using MoqaLate.InterfaceTextParsing;
using MoqaLate.IO;
using MoqaLate.MockClassBuilding;

namespace MoqaLate.Infastructure
{
    class DefaultNinjectModule : Ninject.Modules.NinjectModule
    {
       public override void Load()
       {
           Bind<IClassTextBuilder>().To<ClassTextBuilder>();
           Bind<ICodeFileSearcher>().To<CodeFileSearcher>();
           Bind<IFileContentLoader>().To<FileContentLoader>();
           Bind<IFileWriter>().To<FileWriter>();
           Bind<IInterfaceLineTextParser>().To<InterfaceLineTextLineTextParser>();
           Bind<IIgnoredFilesProvider>().To<IgnoredFilesProvider>();
           Bind<ILogger>().To<ConsoleLogger>();
       }
    }
}
