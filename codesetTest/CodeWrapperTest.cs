using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using codeset.Models;

namespace codesetTest
{
    [TestClass]
    public class CodeWrapperTest
    {
        //* Test Methods

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

        // TODO: Add test method to test if a particular extension is actaully installed
        //       Then uninstall it

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
    }
}