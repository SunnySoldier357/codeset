using codeset.Models;
using CommandLine;

namespace codeset
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsed = Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(options => RunCommand(options));
        }

        public static int RunCommand(Options options)
        {
            if (options.ExtensionFile != null && options.InstallAll)
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallAllExtensions(options.ExtensionFile);
            }

            return 0;
        }
    }
}