using CommandLine;

using codeset.Models;
using codeset.Services;

namespace codeset
{
    public class Application : IApplication
    {
        //* Private Properties
        private readonly ICommandService commandService;

        public void Run(string[] args)
        {
            var parsed = Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(options => commandService.HandleCommand(options));
        }
    }
}