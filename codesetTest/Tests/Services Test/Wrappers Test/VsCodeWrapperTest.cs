using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Linq;

using codeset.Models;
using codeset.Services.Wrappers;
using codeset.Services;

using codesetTest.Utilities;

namespace codesetTest.Tests.ServicesTest.WrappersTest
{
    [TestClass]
    public class VsCodeWrapperTest
    {
        //* Test Methods

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstallExtensionNullTest() => setUpInstallExtension(null);

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InstallExtensionEmptyTest() => setUpInstallExtension("");

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UninstallExtensionNullTest() => setUpUninstallExtension(null);

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UninstallExtensionEmptyTest() => setUpUninstallExtension("");

        /// <summary>
        /// Tests if the InstallExtension() method can correctly install a
        /// particular extension, then tests if the UninstallExtension() method
        /// can correctly uninstall that extension.
        /// </summary>
        [TestMethod]
        public void InstallAndUninstallExtensionTest()
        {
            // Arrange
            string extension = "jsiwhitehead.vscode-maraca";

            var configWrapper = new MockConfigWrapper();
            var terminalWrapper = new MockTerminalWrapper();

            terminalWrapper.InstallCommandExecuted += e =>
            {
                configWrapper.Extensions["Required"].Add(e.ExtensionName);
            };
            terminalWrapper.UninstallCommandExecuted += e =>
            {
                configWrapper.Extensions["Required"].Remove(e.ExtensionName);
            };

            var codeWrapper = new VsCodeWrapper(configWrapper, terminalWrapper);

            var extensionsQuery = configWrapper.Extensions
                .SelectMany(k => k.Value);

            var extensionsBefore = extensionsQuery.ToList();

            // Act
            codeWrapper.InstallExtension(extension);
            var extensionsAfterInstall = extensionsQuery.ToList();

            codeWrapper.UninstallExtension(extension);
            var extensionsAfter = extensionsQuery.ToList();

            // Assert
            Assert.IsFalse(extensionsBefore.Contains(extension));
            Assert.IsTrue(extensionsAfterInstall.Contains(extension));
            Assert.IsFalse(extensionsAfter.Contains(extension));
        }

        [TestMethod]
        public void GetUserSettingsTest()
        {
            // Arrange
            string fileName = "settings";

            var platformService = new MockPlatformService(OSPlatform.Linux);

            JObject settings = createSetting(platformService);

            string path = FileUtility.CreateFile(fileName, FileExtension.Json,
                settings.ToString().Split('\n'));

            var configWrapper = new MockConfigWrapper();
            var terminalWrapper = new MockTerminalWrapper();
            var settingsService = new MockSettingsService(null, path);

            var codeWrapper = new VsCodeWrapper(configWrapper, terminalWrapper,
                settingsService);

            // Act
            var result = codeWrapper.GetUserSettings();

            // Assert
            Assert.AreEqual(settings.ToString(), result.ToString(), false);

            // Cleanup
            FileUtility.DeleteFile(fileName, FileExtension.Json);
        }

        [TestMethod]
        public void WriteUserSettingsTest()
        {
            // Arrange
            string fileName = "settings";

            var platformService = new MockPlatformService(OSPlatform.Linux);

            JObject settings = createSetting(platformService);

            string[] expected = new string[]
            {
                "{",
                "\t//* Required",
                "\t\"testKey 1\": \"item 1\"",
                "\t\"testKey 2\": \"item 2\"",
                "}",
            };
            string expectedString = string.Join('\n', expected);

            string path = FileUtility.CreateFile(fileName, FileExtension.Json,
                new string[] { "" });

            var configWrapper = new MockConfigWrapper();
            var terminalWrapper = new MockTerminalWrapper();
            var settingsService = new MockSettingsService(null, path);

            var codeWrapper = new VsCodeWrapper(configWrapper, terminalWrapper,
                settingsService);

            // Act
            codeWrapper.WriteUserSettings(settings);
            string result = FileUtility.ReadFile(fileName, FileExtension.Json);

            // Assert
            Assert.AreEqual(expectedString, result, false);
        }

        [TestMethod]
        public void UpdateSettingsTest()
        {
            // Arrange
            string fileName = "settings";

            var platformService = new MockPlatformService(OSPlatform.Linux);

            JObject settings = createSetting(platformService);

            string path = FileUtility.CreateFile(fileName, FileExtension.Json,
                settings.ToString().Split('\n'));

            var configWrapper = new MockConfigWrapper();
            var terminalWrapper = new MockTerminalWrapper();
            var settingsService = new MockSettingsService(null, path);

            var codeWrapper = new VsCodeWrapper(configWrapper, terminalWrapper,
                settingsService);

            // TODO: Proper Testing

            // Act
            codeWrapper.UpdateSettings();
        }

        //* Private Methods
        private JObject createSetting(IPlatformService platformService)
        {
            return JObject.FromObject(new Dictionary<string, List<Setting>>
            {
                ["Required"] =
                {
                    new Setting("testKey.one", JToken.FromObject("item 1"),
                        null, platformService),
                    new Setting("testKey.two", JToken.FromObject("item 2"),
                        null, platformService)
                }
            });
        }

        private void setUpInstallExtension(string command)
        {
            // Arrange
            var configWrapper = new MockConfigWrapper();
            var terminalWrapper = new MockTerminalWrapper();

            var codeWrapper = new VsCodeWrapper(configWrapper, terminalWrapper);

            // Act
            codeWrapper.InstallExtension(command);
        }

        private void setUpUninstallExtension(string command)
        {
            // Arrange
            var configWrapper = new MockConfigWrapper();
            var terminalWrapper = new MockTerminalWrapper();

            var codeWrapper = new VsCodeWrapper(configWrapper, terminalWrapper);

            // Act
            codeWrapper.UninstallExtension(command);
        }
    }
}