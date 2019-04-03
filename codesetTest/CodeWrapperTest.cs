using System;
using System.IO;
using codeset.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace codesetTest
{
    [TestClass]
    public class CodeWrapperTest
    {
        [TestMethod]
        public void InstallExtensionNullTest()
        {
            bool caughtException = false;
            CodeWrapper code = new CodeWrapper();

            try
            {
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

        [TestMethod]
        public void InstallExtensionEmptyTest()
        {
            bool caughtException = false;
            CodeWrapper code = new CodeWrapper();

            try
            {
                code.InstallExtension("");
            }
            catch (FileNotFoundException)
            {
                caughtException = true;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsTrue(caughtException);
        }

        [TestMethod]
        public void InstallAllExtensionsNullTest()
        {
            bool caughtException = false;
            CodeWrapper code = new CodeWrapper();

            try
            {
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

        [TestMethod]
        public void InstallAllExtensionsEmptyTest()
        {
            bool result = false;
            CodeWrapper code = new CodeWrapper();

            try
            {
                result = code.InstallAllExtensions("");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsFalse(result);
        }
    }
}