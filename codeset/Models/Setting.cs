using System;
using Newtonsoft.Json.Linq;

namespace codeset.Models
{
    public class Setting
    {
        //* Public Properties
        public string Instruction { get; }
        public string Key { get; }

        public JToken Value { get; set; }

        public JToken ValueForOS
        {
            get
            {
                JToken result = null;

                if (Value.Type == JTokenType.String)
                    result = Value;
                else if (Utility.IsOsWindows())
                {
                    var temp = Value["windows"];
                    if (temp != null)
                        result = temp;
                }
                else if (Utility.IsOsOsx())
                {
                    var temp = Value["osx"];
                    if (temp != null)
                        result = temp;
                }
                else if (Utility.IsOsLinux())
                {
                    var temp = Value["linux"];
                    if (temp != null)
                        result = temp;
                }

                return result;
            }
        }

        //* Constructors
        public Setting(JObject setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            Instruction = setting["instruction"]?.ToString();
            Key = setting["key"].ToString();

            if (Instruction == null)
                Value = setting["value"];
        }
    }
}