using System;
using codeset.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace codesetTest
{
    [TestClass]
    public class ConfigWrapperTest
    {
        [TestMethod]
        public void ConstructorEmptyTest()
        {
            bool caughtException = false;
            ConfigWrapper wrapper = null;

            try
            {
                wrapper = new ConfigWrapper("");
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
    }
}