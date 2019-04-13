using System.Runtime.InteropServices;

namespace codeset.Models
{
    public static class Utility
    {
        //* Private Static Properties
        private static OSPlatform? currentOs = null;

        //* Public Static Properties
        public static OSPlatform CurrentOs
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
        public static bool IsOsLinux() => CurrentOs.Equals(OSPlatform.Linux);
        public static bool IsOsOsx() => CurrentOs.Equals(OSPlatform.OSX);
        public static bool IsOsWindows() => CurrentOs.Equals(OSPlatform.Windows);
    }
}