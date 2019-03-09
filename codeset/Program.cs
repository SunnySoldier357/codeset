﻿using System;
using codeset.Models;
using CommandLine;

namespace codeset
{
    class Program
    {
        static void Main(string[] args)
        {
            //* Steps to test the application
            // dotnet publish -c release -r linux-x64
            // chmod 777 bin/release/netcoreapp2.1/linux-x64/publish/codeset
            // bin/release/netcoreapp2.1/linux-x64/publish/codeset

            var result = FileWrapper.ReadExtensions("/home/sandeepsingh/Repos/Configuration/VS Code/extensions.txt");

            var parsed = Parser.Default
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