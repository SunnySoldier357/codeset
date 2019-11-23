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
    
    public class PlatformService : IPlatformService
    {
        //* Private Properties
        private OSPlatform? currentOs = null;

        //* Public Properties
        public OSPlatform CurrentOs
        {
            get
            {
                if (currentOs == null)
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        currentOs = OSPlatform.Windows;
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        currentOs = OSPlatform.OSX;
                    else
                        currentOs = OSPlatform.Linux;
                }
                
                return (OSPlatform) currentOs;
            }
        }

        //* Public Methods
        public bool IsOsLinux() => CurrentOs.Equals(OSPlatform.Linux);
        public bool IsOsOsx() => CurrentOs.Equals(OSPlatform.OSX);
        public bool IsOsWindows() => CurrentOs.Equals(OSPlatform.Windows);
    }
}