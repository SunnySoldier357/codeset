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
        public Dictionary<string, List<string>> Extensions => getExtensions();
        public Dictionary<string, List<Setting>> Settings => getSettings();

        public List<string> Categories => getCategories();

        //* Constructor
        public ConfigWrapper(IPlatformService platformService, ISettingsService settingsService)
        {
            this.platformService = platformService;
            this.settingsService = settingsService;
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

        private List<string> getCategories()
        {
            JToken json = null;

            using (FileStream fileStream = new FileStream(settingsService.ConfigPath,
                FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fileStream))
            using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
            {
                JObject configFile = (JObject) JToken.ReadFrom(jsonTextReader);

                json = configFile[nameof(Categories).ToLower()];
            }

            List<string> result;

            if (json != null)
                result = convertCategoriesToList(json);
            else
                result = new List<string>();

            result.Add("Required");
            return result;
        }

        private Dictionary<string, List<string>> getExtensions()
        {
            JToken json = null;

            using (FileStream fileStream = new FileStream(settingsService.ConfigPath,
                FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fileStream))
            using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
            {
                JObject configFile = (JObject) JToken.ReadFrom(jsonTextReader);

                json = configFile[nameof(Extensions).ToLower()];
            }

            JObject extensions;

            if (json == null)
                return null;
            else
            {
                if (json is JObject)
                    extensions = (JObject) json;
                else
                {
                    // Get the JSON file at that path and set extensions to that
                    using (FileStream fileStream = new FileStream(json.ToString(),
                        FileMode.Open))
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
                    {
                        extensions = (JObject) JToken.ReadFrom(jsonTextReader);
                    }
                }

                return convertExtensionsToDic(extensions);
            }
        }

        private Dictionary<string, List<Setting>> getSettings()
        {
            JToken json = null;

            using (FileStream fileStream = new FileStream(settingsService.ConfigPath,
                FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fileStream))
            using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
            {
                JObject configFile = (JObject) JToken.ReadFrom(jsonTextReader);

                json = configFile[nameof(Settings).ToLower()];
            }

            JObject settings;

            if (json == null)
                return null;
            else
            {
                if (json is JObject)
                    settings = (JObject) json;
                else
                {
                    // Get the JSON file at that path and set settings to that
                    using (FileStream fileStream = new FileStream(json.ToString(),
                        FileMode.Open))
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
                    {
                        settings = (JObject) JToken.ReadFrom(jsonTextReader);
                    }
                }

                return convertSettingsToDic(settings);
            }
        }
    }
}