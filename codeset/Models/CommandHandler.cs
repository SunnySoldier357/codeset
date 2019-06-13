using codeset.Wrappers;

namespace codeset.Models
{
    public class CommandHandler
    {
        //* Public Methods
        public int HandleCommand(Options options)
        {
            if (options.UpdateExtension)
            {
                VsCodeWrapper code = new VsCodeWrapper();
                code.InstallAllExtensions(new ConfigWrapper());
            }
            else if (options.UpdateSettings)
            {
                VsCodeWrapper code = new VsCodeWrapper();
                code.UpdateSettings(new ConfigWrapper());
            }

            return 0;
        }
    }
}