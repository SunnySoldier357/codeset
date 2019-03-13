﻿using codeset.Models;
using CommandLine;

namespace codeset
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandHandler cmdHandler = new CommandHandler();

            var parsed = Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(options => cmdHandler.HandleCommand(options));
        }
    }
}