namespace codeset.Models
{
    public class CommandHandler
    {
        //* Public Methods
        public int HandleCommand(Options options, ConfigWrapper wrapper)
        {
            if (options.UpdateExtension)
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallAllExtensions(wrapper);
            }
            else if (options.UpdateSettings)
            {
                CodeWrapper code = new CodeWrapper();
                code.UpdateSettings(wrapper);
            }

            return 0;
        }
    }
}