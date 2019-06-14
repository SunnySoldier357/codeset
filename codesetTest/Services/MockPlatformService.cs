using System.Runtime.InteropServices;

namespace codeset.Services
{
    public class MockPlatformService : IPlatformService
    {
        //* Private Properties
        private OSPlatform currentOs;

        //* Public Properties
        public OSPlatform CurrentOs => currentOs;

        //* Constructors
        public MockPlatformService(OSPlatform platform) => currentOs = platform;

        //* Public Methods
        public bool IsOsLinux() => CurrentOs.Equals(OSPlatform.Linux);
        public bool IsOsOsx() => CurrentOs.Equals(OSPlatform.OSX);
        public bool IsOsWindows() => CurrentOs.Equals(OSPlatform.Windows);
    }
}