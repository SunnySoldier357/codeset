namespace codeset.Services
{
    public class MockSettingsService : ISettingsService
    {
        //* Public Properties
        public string ConfigPath { get; set; }
        public string UserSettingsPath { get; set; }

        //* Constructors
        public MockSettingsService(string configPath) : this(configPath, null) { }

        public MockSettingsService(string configPath, string userSettingsPath)
        {
            ConfigPath = configPath;
            UserSettingsPath = userSettingsPath;
        }
    }
}