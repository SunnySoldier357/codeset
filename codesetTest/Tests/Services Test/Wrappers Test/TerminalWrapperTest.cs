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

        /// <summary>
        /// <para>
        /// Tests if the Execute() method can correctly handle null command.
        /// </para>
        /// <para>
        /// Input: null for command
        /// </para>
        /// <para>
        /// Expected Output: ArgumentNullException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteNullTest() => setUpExecuteMethod(null);

        /// <summary>
        /// <para>
        /// Tests if the Execute() method can correctly handle empty command.
        /// </para>
        /// <para>
        /// Input: "" for command
        /// </para>
        /// <para>
        /// Expected Output: ""
        /// </para>
        /// </summary>
        [TestMethod]
        public void ExecuteEmptyTest()
        {
            // Arrange & Act
            string result = setUpExecuteMethod("");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        /// <summary>
        /// <para>
        /// Tests if the Execute() method can correctly handle a proper command
        /// input.
        /// </para>
        /// <para>
        /// Input: "echo 'testing'" for command
        /// </para>
        /// <para>
        /// Expected Output: "testing"
        /// </para>
        /// </summary>
        [TestMethod]
        public void ExecuteEchoTest()
        {
            // Arrange & Act
            string result = setUpExecuteMethod("echo 'testing'");

            // Assert
            Assert.IsTrue(result == "testing");
        }

        /// <summary>
        /// <para>
        /// Tests if the Execute() method behaves correctly by executing
        /// consecutive commands.
        /// </para>
        /// <para>
        /// Input 1: "echo 'testing'" for command
        /// Input 1: "echo 'testing 2'" for command
        /// </para>
        /// <para>
        /// Expected Output 1: "testing"
        /// Expected Output 2: "testing"
        /// </para>
        /// </summary>
        [TestMethod]
        public void ExecuteMultipleEchoTest()
        {
            // Arrange
            var platformService = new PlatformService();
            TerminalWrapper terminal = new TerminalWrapper(platformService);

            // Act
            string result = terminal.Execute("echo 'testing'");
            string result2 = terminal.Execute("echo 'testing 2'");

            // Assert
            Assert.IsTrue(result == "testing");
            Assert.IsTrue(result2 == "testing 2");
        }

        //* Private Methods
        private string setUpExecuteMethod(string command)
        {
            // Arrange
            var platformService = new PlatformService();
            TerminalWrapper terminal = new TerminalWrapper(platformService);

            // Act
            return terminal.Execute(command);
        }
    }
}