namespace codeset.Models
{
    public class CommandHandler
    {
        //* Public Methods
        public int HandleCommand(Options options, ConfigWrapper wrapper)
        {
            if (options.ExtensionFile)
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallAllExtensions(wrapper);
            }

            return 0;
        }
    }
}