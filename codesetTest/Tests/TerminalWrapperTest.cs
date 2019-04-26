using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using codeset.Models;

namespace codesetTest.Tests
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
        public void ExecuteNullTest()
        {
            bool caughtException = false;

            try
            {
                TerminalWrapper terminal = new TerminalWrapper();
                terminal.Execute(null);
            }
            catch (ArgumentNullException)
            {
                caughtException = true;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsTrue(caughtException);
        }

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
            try
            {
                TerminalWrapper terminal = new TerminalWrapper();
                string result = terminal.Execute("");

                Assert.IsNotNull(result);
                Assert.IsTrue(string.IsNullOrEmpty(result));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
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
            try
            {
                TerminalWrapper terminal = new TerminalWrapper();
                string result = terminal.Execute("echo 'testing'");

                Assert.IsNotNull(result);
                Assert.IsTrue(result == "testing");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
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
            try
            {
                TerminalWrapper terminal = new TerminalWrapper();
                string result = terminal.Execute("echo 'testing'");

                Assert.IsNotNull(result);
                Assert.IsTrue(result == "testing");

                result = terminal.Execute("echo 'testing 2'");

                Assert.IsNotNull(result);
                Assert.IsTrue(result == "testing 2");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}