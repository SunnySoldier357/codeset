using System;
using System.IO;

namespace codeset.Services
{
    public interface ISettingsService
    {
        string ConfigPath { get; }
        string UserSettingsPath { get; }
    }

    public class SettingsService : ISettingsService
    {
        //* Private Properties
        private string configPath;
        private string userSettingsPath;

        //* Public Properties
        public string ConfigPath
        {
            get
            {
                if (configPath == null)
                {
                    string home = Environment
                        .GetFolderPath(Environment.SpecialFolder.UserProfile);

                    string path = Path.Combine(home, ".config",
                        "codeset", "config.json");

                    FileInfo file = new FileInfo(path);

                    if (file.Exists)
                        configPath = file.FullName;
                }

                return configPath;
            }
        }
        public string UserSettingsPath => userSettingsPath;

        //* Constructors
        public SettingsService(IPlatformService platformService)
        {
            string path = "";

            if (platformService.IsOsWindows())
            {
                path = Environment
                    .GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
            else
            {
                string home = Environment
                    .GetFolderPath(Environment.SpecialFolder.UserProfile);
                
                if (platformService.IsOsLinux())
                    path = Path.Combine(home, ".config");
                else
                    path = Path.Combine(home, "Library", "Application Support");
            }

            userSettingsPath = Path.Combine(path, "Code", "User", "settings.json");
        }
    }
}