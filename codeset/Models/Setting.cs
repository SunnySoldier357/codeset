using System.Collections.Generic;

namespace codeset.Models
{
    public class Setting
    {
        //* Private Properties
        private string value;

        //* Public Properties
        public string Key { get; }

        public string Value { get; }

        public string Instruction { get; set; }

        //* Constructors
        public Setting()
        {
            Value = getValueForOs();
        }

        //* Private Methods
        private string getValueForOs()
        {
            // If value is formatted like Windows:custom; Linux:native then extract
            // the correct value for the current OS

            return "";
        }
    }
}