using CommandLine;

namespace codeset.Models
{
    public class Options
    {
        //* Public Properties

        [Option("extfile",
            HelpText = "The txt file that contains a list of extensions.")]
        public string ExtensionFile { get; set; }

        public string SettingFile { get; set; }
    }
}