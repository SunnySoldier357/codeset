namespace codeset.Services
{
    public interface ISettingsService
    {
        string ConfigPath { get; }
        string UserSettingsPath { get; }
    }
}