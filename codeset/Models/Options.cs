using CommandLine;

namespace codeset.Models
{
    public class Options
    {
        //* Public Properties

        [Option("extfile",
            HelpText = "The txt file that contains a list of extensions.")]
        public string ExtensionFile { get; set; }

        [Option("setfile",
            HelpText = "The JSON file that contains the required settings formatted correctly")]
        public string SettingFile { get; set; }
    }
}