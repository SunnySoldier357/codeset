using CommandLine;

using codeset.Models;
using codeset.Services;

namespace codeset
{
    public interface IApplication
    {
        //* Interface Methods
        void Run(string[] args);
    }
    
    public class Application : IApplication
    {
        //* Private Properties
        private readonly ICommandService commandService;

        //* Constructors
        public Application(ICommandService commandService) =>
            this.commandService = commandService;

        //* Public Methods
        public void Run(string[] args)
        {
            var parsed = Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(options => commandService.HandleCommand(options));
        }
    }
}