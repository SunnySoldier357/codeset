using System.Runtime.InteropServices;

namespace codeset.Models
{
    public static class Utility
    {
        //* Private Static Properties
        private static OSPlatform? currentOS = null;

        //* Public Static Properties
        public static OSPlatform CurrentOS
        {
            get
            {
                if (currentOS == null)
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        currentOS = OSPlatform.Windows;
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        currentOS = OSPlatform.OSX;
                    else
                        currentOS = OSPlatform.Linux;
                }
                
                return (OSPlatform) currentOS;
            }
        }
    }
}