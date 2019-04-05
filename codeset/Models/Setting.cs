using System.Collections.Generic;
using System.Runtime.InteropServices;
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
            if (lines != null && lines.Count >= 1)
            {
                int i = 0;

                foreach (string line in lines)
                {
                    if (line.Contains("// TODO:"))
                    {
                        Instruction = line.Replace("// TODO:", "").Trim();
                    }
                    else
                    {
                        if (Key == null)
                        {
                            int index = line.IndexOf(':');
                            Key = line.Substring(0, index).Trim();
                            string tempValue = line.Substring(index + 1).Trim();

                            if (i == lines.Count - 1)
                            {
                                if (tempValue.Contains(':'))
                                    Value = getValueForOs(tempValue);
                                else
                                    Value = tempValue.Replace(",", "");
                            }
                            else
                                Value = tempValue;
                        }
                        else
                        {
                            if (i == lines.Count - 1)
                                Value += "\n" + line.Replace(",", "");
                            else
                                Value += "\n" + line;
                        }
                    }

                    i++;
                }
            }

            if (Instruction != null)
                Value = null;
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