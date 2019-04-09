using CommandLine;

using codeset.Models;

namespace codeset
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandHandler cmdHandler = new CommandHandler();
            ConfigWrapper wrapper = new ConfigWrapper();

            var parsed = Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(options => cmdHandler.HandleCommand(options, wrapper));
        }
    }
}