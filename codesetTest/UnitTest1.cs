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

        [TestMethod]
        public void ReadExtensionsNullTest()
        {
            Dictionary<string, List<string>> result = null;

            try
            {
                result = FileWrapper.ReadExtensions(null);
            }
            catch (Exception)
            {
                Assert.Fail("Null File Path caused an error");
            }

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReadExtensionsSimpleAlgorithmTest()
        {
            // TODO: Create a new file and use that to check the algorithm
            Assert.Inconclusive();
        }
    }
}
