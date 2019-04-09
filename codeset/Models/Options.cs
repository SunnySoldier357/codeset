using CommandLine;

namespace codeset.Models
{
    public class Options
    {
        //* Public Properties

        [Option("install-extensions",
            HelpText = "Installs the extensions per the config.json file.")]
        public bool ExtensionFile { get; set; }
    }
}