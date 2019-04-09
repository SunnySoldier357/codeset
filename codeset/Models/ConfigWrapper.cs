using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace codeset.Models
{
    public class ConfigWrapper
    {
        //* Private Static Properties
        private static string configPath = null;

        //* Public Static Properties
        public static string ConfigPath
        {
            get
            {
                if (configPath == null)
                {
                    string path = Environment.GetEnvironmentVariable("HOME");

                    DirectoryInfo dir = new DirectoryInfo(path);

                    dir = dir.GetDirectories().FirstOrDefault(d => d.Name == ".config");
                    dir = dir.GetDirectories().FirstOrDefault(d => d.Name == "codeset");
                    var configFile = dir.GetFiles().FirstOrDefault(f => f.Name == "config.json");

                    configPath = configFile.FullName;
                }
                
                return configPath;
            }
        }

        //* Public Properties
        public Dictionary<string, List<string>> Extensions { get; private set; }
        public Dictionary<string, List<Setting>> Settings { get; private set; }

        //* Constructor
        public ConfigWrapper(string path = null)
        {
            // Used for testing
            if (path == null)
                path = ConfigPath;

            JToken configJson = null;
            JObject extensions = null;

            using (StreamReader stream = new StreamReader(
                new FileStream(ConfigPath, FileMode.Open)))
            {
                JObject configFile = (JObject) JToken.ReadFrom(new JsonTextReader(stream));
                configJson = configFile["extensions"];
            }

            if (configJson is JObject)
                extensions = (JObject) configJson;
            else
            {
                // Get the JSON file at that path and set extensions to that
                using (StreamReader stream = new StreamReader(
                    new FileStream(configJson.ToString(), FileMode.Open)))
                {
                    extensions = (JObject) JToken.ReadFrom(new JsonTextReader(stream));
                }
            }

            Extensions = convertExtensionsToDic(extensions);
        }

        //* Private Methods
        private Dictionary<string, List<string>> convertExtensionsToDic(JObject extensions)
        {
            var dictionary = new Dictionary<string, List<string>>();

            var properties = extensions.Properties();

            foreach (JProperty property in properties)
            {
                var values = new List<string>();

                foreach (var value in (JArray) property.Value)
                    values.Add(value.ToString());

                dictionary[property.Name] = values;
            }

            return dictionary;
        }
    }
}