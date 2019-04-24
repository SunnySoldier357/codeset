namespace codeset.Models
{
    public class CommandHandler
    {
        //* Public Methods
        public int HandleCommand(Options options)
        {
            if (options.UpdateExtension)
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallAllExtensions(new ConfigWrapper());
            }
            else if (options.UpdateSettings)
            {
                CodeWrapper code = new CodeWrapper();
                code.UpdateSettings(new ConfigWrapper());
            }

            return 0;
        }
    }
}