using System;
using System.Collections.Generic;
using codeset.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace codesetTest
{
    [TestClass]
    public class FileWrapperTest
    {
        [TestMethod]
        public void ReadExtensionsEmptyTest()
        {
            Dictionary<string, List<string>> result = null;

            try
            {
                result = FileWrapper.ReadExtensions("");
            }
            catch (Exception)
            {
                Assert.Fail("Empty File Path caused an error");
            }

            Assert.IsNull(result);
        }
    }
}
