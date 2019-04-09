using Newtonsoft.Json.Linq;

namespace codeset.Models
{
    public class Setting
    {
        //* Public Properties
        public string Instruction { get; }
        public string Key { get; }
        public JObject Value { get; set; }

        //* Constructors
        public Setting(JObject setting)
        {
            // TODO: Initialise variables
        }
    }
}