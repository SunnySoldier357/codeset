using System;
using codeset.Models;
using CommandLine;

namespace codeset
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(options => RunCommand(options));
        }

        public static int RunCommand(Options options)
        {
            if (options.PrintHello)
                Console.WriteLine("Hello World!");

            return 0;
        }
    }
}