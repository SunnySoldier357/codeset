using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using codeset.Services.Wrappers;

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

        // TODO: Add tests for UpdateSettings()

        //* Private Methods
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