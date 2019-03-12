using CommandLine;

namespace codeset.Models
{
    public class Options
    {
        //* Public Properties

        [Option("install-extensions",
            HelpText = "The txt file that contains a list of extensions.")]
        public string ExtensionFile { get; set; }

        [Option('a', "all", HelpText = "Installs all of the extensions")]
        public bool InstallAll { get; set; }

        [Option("setfile",
            HelpText = "The JSON file that contains the required settings formatted correctly")]
        public string SettingFile { get; set; }
    }
}