using System;

using Newtonsoft.Json.Linq;

using codeset.Services;

namespace codeset.Models
{
    public class Setting
    {
        //* Private Properties
        private readonly IPlatformService platformService;

        //* Public Properties
        public string Instruction { get; }
        public string Key { get; }

        public JToken Value { get; set; }

        public JToken ValueForOS
        {
            get
            {
                if (Value == null)
                    return null;

                JToken result = null;

                if (Value.Type != JTokenType.Object)
                    result = Value;
                else if (platformService.IsOsWindows())
                {
                    var temp = Value["windows"];
                    if (temp != null)
                        result = temp;
                }
                else if (platformService.IsOsOsx())
                {
                    var temp = Value["osx"];
                    if (temp != null)
                        result = temp;
                }
                else if (platformService.IsOsLinux())
                {
                    var temp = Value["linux"];
                    if (temp != null)
                        result = temp;
                }

                return result;
            }
        }

        //* Constructors
        public Setting(JObject setting, IPlatformService platformService)
        {
            this.platformService = platformService;

            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            Instruction = setting["instruction"]?.ToString();
            Key = setting["key"].ToString();

            if (Instruction == null)
                Value = setting["value"];
            else
                Value = null;
        }

        public Setting(string key, JToken value, string instruction,
            IPlatformService platformService)
        {
            Key = key;
            Value = value;
            Instruction = instruction;

            this.platformService = platformService;
        }

        //* Overridden Methods
        public override bool Equals(object obj)
        {
            if (obj is Setting other)
            {
                return Instruction == other?.Instruction &&
                    Key == other?.Key &&
                    Value.Equals(other?.Value);
            }

            return false;
        }

        public override int GetHashCode() =>
            Instruction?.GetHashCode() + Key?.GetHashCode() +
                Value?.GetHashCode() ?? 0;
    }
}