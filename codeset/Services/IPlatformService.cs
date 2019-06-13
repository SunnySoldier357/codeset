using System.Runtime.InteropServices;

namespace codeset.Services
{
    public interface IPlatformService
    {
        //* Interface Properties
        OSPlatform CurrentOs { get; }

        //* Interface Methods
        bool IsOsLinux();
        bool IsOsOsx();
        bool IsOsWindows();
    }
}