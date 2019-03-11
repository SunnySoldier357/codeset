using System;
using codeset.Models;
using CommandLine;

namespace codeset
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = FileWrapper.ReadExtensions("/home/sandeepsingh/Repos/Configuration/VS Code/extensions.txt");

            var parsed = Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(options => RunCommand(options));
        }

        public static int RunCommand(Options options)
        {
            return 0;
        }
    }
}