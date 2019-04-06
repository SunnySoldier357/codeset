using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

using codeset.Models;

namespace codesetTest
{
    [TestClass]
    public class CodeWrapperTest
    {
        /// <summary>
        /// <para>
        /// Tests if the InstallExtension() method can correctly handle null
        /// extension name.
        /// </para>
        /// <para>
        /// Input: null for extension
        /// </para>
        /// <para>
        /// Expected Output: ArgumentNullException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void InstallExtensionNullTest()
        {
            bool caughtException = false;
            
            try
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallExtension(null);
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
        /// Tests if the InstallExtension() method can correctly handle empty
        /// extension name.
        /// </para>
        /// <para>
        /// Input: "" for extension
        /// </para>
        /// <para>
        /// Expected Output: ArgumentException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void InstallExtensionEmptyTest()
        {
            bool caughtException = false;

            try
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallExtension("");
            }
            catch (ArgumentException)
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
        /// Tests if the InstallAllExtension() method can correctly handle null
        /// path.
        /// </para>
        /// <para>
        /// Input: null for path
        /// </para>
        /// <para>
        /// Expected Output: ArgumentNullException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void InstallAllExtensionsNullTest()
        {
            bool caughtException = false;
            
            try
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallAllExtensions(null);
            }
            catch (ArgumentNullException)
            {
                caughtException = true;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsFalse(caughtException);
        }

        /// <summary>
        /// <para>
        /// Tests if the InstallExtension() method can correctly handle empty path.
        /// </para>
        /// <para>
        /// Input: "" for path
        /// </para>
        /// <para>
        /// Expected Output: IOException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void InstallAllExtensionsEmptyTest()
        {
            bool caughtException = false;

            try
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallAllExtensions("");
            }
            catch (IOException)
            {
                caughtException = true;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsTrue(caughtException);
        }
    }
}