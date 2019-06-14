namespace codeset.Services
{
    public class MockSettingsService : ISettingsService
    {
        //* Private Properties
        private string configPath;

        //* Public Properties
        public string ConfigPath => configPath;

        //* Constructors
        public MockSettingsService(string configPath) =>
            this.configPath = configPath;
    }
}