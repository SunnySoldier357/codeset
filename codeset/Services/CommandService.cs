using codeset.Models;
using codeset.Services.Wrappers;

namespace codeset.Services
{
    public class CommandService : ICommandService
    {
        //* Private Properties
        private readonly IVsCodeWrapper vsCodeWrapper;

        //* Constructors
        public CommandService(IVsCodeWrapper vsCodeWrapper) =>
            this.vsCodeWrapper = vsCodeWrapper;

        //* Public Methods
        public int HandleCommand(Options options)
        {
            if (options.UpdateExtension)
                vsCodeWrapper.UpdateExtensions();
            else if (options.UpdateSettings)
                vsCodeWrapper.UpdateSettings();

            return 0;
        }
    }
}