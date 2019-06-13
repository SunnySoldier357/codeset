using System.Collections.Generic;

namespace codeset.Services.Wrappers
{
    public interface IVsCodeWrapper
    {
        //* Interface Methods
        List<string> GetExtensions();

        void InstallExtension(string extension);
        void UninstallExtension(string extension);
        void UpdateExtensions();

        void UpdateSettings();
    }
}