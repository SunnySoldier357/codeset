using System;
using System.IO;
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

            try
            {
                ConfigWrapper wrapper = new ConfigWrapper("");
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