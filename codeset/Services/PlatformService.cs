using System.Runtime.InteropServices;

namespace codeset.Services
{
    public class PlatformService : IPlatformService
    {
        //* Private Static Properties
        private OSPlatform? currentOs = null;

        //* Public Static Properties
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

        //* Public Static Methods
        public bool IsOsLinux() => CurrentOs.Equals(OSPlatform.Linux);
        public bool IsOsOsx() => CurrentOs.Equals(OSPlatform.OSX);
        public bool IsOsWindows() => CurrentOs.Equals(OSPlatform.Windows);
    }
}