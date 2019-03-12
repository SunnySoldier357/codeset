using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace codeset.Models
{
    public class Setting
    {
        //* Public Properties
        public string Instruction { get; }
        public string Key { get; }
        public string Value { get; set; }

        //* Constructors
        public Setting(List<string> lines)
        {
            if (lines != null)
            {
                if (lines.Count == 1)
                {
                    int index = lines[0].IndexOf(':');
                    Key = lines[0].Substring(0, index).Trim();
                    Value = getValueForOs(lines[0].Substring(index + 1).Trim());
                }
                else
                {
                    // Convert lines to a single string with /n separating lines
                }
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
            string result = null;
            OSPlatform os = getOS();

            // If value is formatted like Windows:custom; Linux:native then extract
            // the correct value for the current OS
            if (value.Contains(';'))
            {
                value = value.Replace("\"", "").Replace(",", "");
                
                string[] split = value.Split(';');
                foreach (string item in split)
                {
                    string[] splitFurther = item.Split(':');

                    if ((os.Equals(OSPlatform.Windows) && splitFurther[0].Trim() == "Windows") ||
                        (os.Equals(OSPlatform.OSX) && splitFurther[0].Trim() == "OSX") ||
                        (os.Equals(OSPlatform.Linux) && splitFurther[0].Trim() == "Linux"))
                    {
                        result = string.Format("\"{0}\"", splitFurther[1]);
                        break;
                    }
                }
            }

            return result;
        }
    }
}