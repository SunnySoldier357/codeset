using CommandLine;

namespace codeset.Models
{
    public class Options
    {
        //* Public Properties

        [Option('e', "update-extensions",
            HelpText = "Updates the extensions per the config.json file.")]
        public bool UpdateExtension { get; set; }

        [Option('s', "update-settings",
            HelpText = "Updates the settings per the config.json file.")]
        public bool UpdateSettings { get; set; }
    }
}