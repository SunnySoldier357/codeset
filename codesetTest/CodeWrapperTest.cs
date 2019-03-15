using System;
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
            bool result = false;
            CodeWrapper code = new CodeWrapper();

            try
            {
                result = code.InstallExtension(null);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InstallExtensionEmptyTest()
        {
            bool result = false;
            CodeWrapper code = new CodeWrapper();

            try
            {
                result = code.InstallExtension("");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InstallAllExtensionsNullTest()
        {
            bool result = false;
            CodeWrapper code = new CodeWrapper();

            try
            {
                result = code.InstallAllExtensions(null);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsFalse(result);
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