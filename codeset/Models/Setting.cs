using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace codeset.Models
{
    public class Setting
    {
        //* Public Properties
        public string Key { get; }

        public string Value { get; set; }

        public string Instruction { get; set; }

        //* Constructors
        public Setting(List<string> lines)
        {
            if (lines.Count == 1)
                Value = getValueForOs(lines[0]);
            else
            {
                // Convert lines to a single string with /n separating lines
            }
        }

        //* Private Methods
        private OSPlatform getOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return OSPlatform.Windows;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return OSPlatform.OSX;

            return OSPlatform.Linux;
        }

        private string getValueForOs(string value)
        {
            string result = "";

            // If value is formatted like Windows:custom; Linux:native then extract
            // the correct value for the current OS
            if (Value.Contains(';'))
            {
                string[] items = value.Split(';');
            }

            return result;
        }
    }
}