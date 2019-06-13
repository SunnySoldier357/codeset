using System;
using System.IO;
using System.Linq;

namespace codeset.Services
{
    public class SettingsService : ISettingsService
    {
        //* Private Properties
        private string configPath;

        //* Public Properties
        public string ConfigPath
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
    }
}