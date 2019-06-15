using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using codeset.Services;
using codeset.Services.Wrappers;

namespace codesetTest.Tests.ServicesTest.WrappersTest
{
    [TestClass]
    public class TerminalWrapperTest
    {
        //* Test Methods

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteNullTest() => setUpExecuteMethod(null);

        [TestMethod]
        public void ExecuteEmptyTest()
        {
            // Arrange & Act
            string result = setUpExecuteMethod("");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void ExecuteEchoTest()
        {
            // Arrange & Act
            string result = setUpExecuteMethod("echo testing");

            // Assert
            Assert.IsTrue(result == "testing");
        }

        [TestMethod]
        public void ExecuteMultipleEchoTest()
        {
            // Arrange
            var platformService = new PlatformService();
            var terminalWrapper = new TerminalWrapper(platformService);

            // Act
            string result = terminalWrapper.Execute("echo testing");
            string result2 = terminalWrapper.Execute("echo testing 2");

            // Assert
            Assert.IsTrue(result == "testing");
            Assert.IsTrue(result2 == "testing 2");
        }

        //* Private Methods
        private string setUpExecuteMethod(string command)
        {
            // Arrange
            var platformService = new PlatformService();
            var terminalWrapper = new TerminalWrapper(platformService);

            // Act
            return terminalWrapper.Execute(command);
        }
    }
}