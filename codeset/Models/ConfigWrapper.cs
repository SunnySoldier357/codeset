using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace codeset.Models
{
    public class ConfigWrapper
    {
        //* Private Static Properties
        private static string configPath = "/home/sandeepsingh/.config/codeset/config.json";

        //* Private Properties
        public JObject Extensions { get; private set; }
        public JObject Settings { get; private set; }

        //* Constructor
        public ConfigWrapper(string path = null)
        {
            // Used for testing
            if (path == null)
                path = configPath;

            JToken configJson = null;

            using (StreamReader stream = new StreamReader(
                new FileStream(configPath, FileMode.Open)))
            {
                JObject configFile = (JObject) JToken.ReadFrom(new JsonTextReader(stream));
                configJson = configFile["extensions"];
            }

            if (configJson is JObject)
                Extensions = (JObject) configJson;
            else
            {
                // Get the JSON file at that path and set extensions to that
                using (StreamReader stream = new StreamReader(
                    new FileStream(configJson.ToString(), FileMode.Open)))
                {
                    Extensions = (JObject) JToken.ReadFrom(new JsonTextReader(stream));
                }
            }

        }
    }
}