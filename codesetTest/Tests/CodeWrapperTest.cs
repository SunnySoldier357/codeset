using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using codeset.Models;

namespace codesetTest.Tests
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
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallExtension(null);
            });
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
            Assert.ThrowsException<ArgumentException>(() =>
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallExtension("");
            });
        }

        /// <summary>
        /// <para>
        /// Tests if the InstallExtension() method can correctly install a
        /// particular extension, then tests if the UninstallExtension() method
        /// can correctly uninstall that extension.
        /// </para>
        /// </summary>
        [TestMethod]
        public void InstallAndUninstallExtensionTest()
        {
            string extension = "jsiwhitehead.vscode-maraca";

            try
            {
                CodeWrapper code = new CodeWrapper();

                var extensions = code.GetExtensions();

                if (extensions.Contains(extension))
                    Assert.Fail("Extension already installed before test method began");

                // Testing the install
                code.InstallExtension(extension);
                extensions = code.GetExtensions();

                Assert.IsTrue(extensions.Contains(extension));

                // Testing the uninstall
                code.UninstallExtension(extension);
                extensions = code.GetExtensions();

                Assert.IsFalse(extensions.Contains(extension));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
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
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                CodeWrapper code = new CodeWrapper();
                code.InstallAllExtensions(null);
            });
        }

        /// <summary>
        /// <para>
        /// Tests if the UninstallExtension() method can correctly handle null
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
        public void UninstallExtensionNullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                CodeWrapper code = new CodeWrapper();
                code.UninstallExtension(null);
            });
        }

        /// <summary>
        /// <para>
        /// Tests if the UninstallExtension() method can correctly handle empty
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
        public void UninstallExtensionEmptyTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                CodeWrapper code = new CodeWrapper();
                code.UninstallExtension("");
            });
        }

        // TODO: Add tests for UpdateSettings()
    }
}