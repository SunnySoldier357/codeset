using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using codeset.Models;

namespace codeset.Services.Wrappers
{
    public class ConfigWrapper : IConfigWrapper
    {
        //* Private Properties
        private readonly IPlatformService platformService;

        private readonly ISettingsService settingsService;

        //* Public Properties
        public Dictionary<string, List<string>> Extensions { get; }
        public Dictionary<string, List<Setting>> Settings { get; }

        public List<string> Categories { get; }

        //* Constructor
        public ConfigWrapper(IPlatformService platformService, ISettingsService settingsService)
        {
            this.platformService = platformService;
            this.settingsService = settingsService;

            JToken categoriesJson = null;
            JToken extensionsJson = null;
            JToken settingsJson = null;

            JObject extensions = null;
            JObject settings = null;

            using (StreamReader stream = new StreamReader(
                new FileStream(this.settingsService.ConfigPath, FileMode.Open)))
            {
                JObject configFile = (JObject) JToken.ReadFrom(new JsonTextReader(stream));

                categoriesJson = configFile["categories"];
                extensionsJson = configFile["extensions"];
                settingsJson = configFile["settings"];
            }
            
            if (categoriesJson != null)
                Categories = convertCategoriesToList(categoriesJson);
            else
                Categories = new List<string>();

            Categories.Add("Required");

            if (extensionsJson == null)
                Extensions = null;
            else
            {
                if (extensionsJson is JObject)
                    extensions = (JObject) extensionsJson;
                else
                {
                    // Get the JSON file at that path and set extensions to that
                    using (StreamReader stream = new StreamReader(
                        new FileStream(extensionsJson.ToString(), FileMode.Open)))
                    {
                        extensions = (JObject) JToken.ReadFrom(new JsonTextReader(stream));
                    }
                }

                Extensions = convertExtensionsToDic(extensions);
            }

            if (settingsJson == null)
                Settings = null;
            else
            {
                if (settingsJson is JObject)
                    settings = (JObject) settingsJson;
                else
                {
                    // Get the JSON file at that path and set extensions to that
                    using (StreamReader stream = new StreamReader(
                        new FileStream(settingsJson.ToString(), FileMode.Open)))
                    {
                        settings = (JObject) JToken.ReadFrom(new JsonTextReader(stream));
                    }
                }

                Settings = convertSettingsToDic(settings);
            }
        }

        //* Private Methods
        private List<string> convertCategoriesToList(JToken categories)
        {
            var list = new List<string>();

            JArray array = (JArray) categories;
            foreach (JToken category in array)
                list.Add(category.ToString());

            return list;
        }

        private Dictionary<string, List<string>> convertExtensionsToDic(JObject extensions)
        {
            var dictionary = new Dictionary<string, List<string>>();

            var properties = extensions.Properties();

            foreach (JProperty property in properties)
            {
                var values = new List<string>();

                foreach (JToken value in (JArray) property.Value)
                    values.Add(value.ToString());

                dictionary[property.Name] = values;
            }

            return dictionary;
        }

        private Dictionary<string, List<Setting>> convertSettingsToDic(JObject settings)
        {
            var dictionary = new Dictionary<string, List<Setting>>();

            var categories = settings.Properties();

            foreach (JProperty category in categories)
            {
                var values = new List<Setting>();

                foreach (JToken value in (JArray) category.Value)
                {
                    Setting temp = new Setting((JObject) value, platformService);
                    values.Add(temp);
                }

                dictionary[category.Name] = values;
            }

            return dictionary;
        }
    }
}