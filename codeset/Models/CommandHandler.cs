namespace codeset.Models
{
    public class CommandHandler
    {
        //* Public Methods
        public int HandleCommand(Options options, ConfigWrapper wrapper)
        {
            if (options.ExtensionFile == true)
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallAllExtensions(wrapper);
            }

            return 0;
        }
    }
}