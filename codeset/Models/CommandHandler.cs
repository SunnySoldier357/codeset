namespace codeset.Models
{
    public class CommandHandler
    {
        // Public Methods
        public int HandleCommand(Options options)
        {
            if (options.ExtensionFile != null && options.InstallAll)
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallAllExtensions(options.ExtensionFile);
            }

            return 0;
        }
    }
}