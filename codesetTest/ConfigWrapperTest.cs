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
            ConfigWrapper wrapper = null;

            try
            {
                wrapper = new ConfigWrapper("");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsNotNull(wrapper);

            Assert.IsNull(wrapper.Extensions);
            Assert.IsNull(wrapper.Settings);
        }
    }
}