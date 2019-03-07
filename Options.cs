using CommandLine;
using CommandLine.Text;

namespace codeset
{
    public class Options
    {
        //* Public Properties

        [ParserState]
        public IParserState LastParserState { get; set; }

        [Option("extfile",
            HelpText = "The txt file that contains a list of extensions.")]
        public string ExtensionFile { get; set; }

        public string SettingFile { get; set; }

        //* Public Methods

        [HelpOption]
        public string GetUsage()
        {
            
        }
    }
}