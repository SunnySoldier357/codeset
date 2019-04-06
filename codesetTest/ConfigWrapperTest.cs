using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

using codeset.Models;

namespace codesetTest
{
    [TestClass]
    public class ConfigWrapperTest
    {
        /// <summary>
        /// <para>
        /// Tests if the Constructor method can correctly handle empty path.
        /// </para>
        /// <para>
        /// Input: "" for path
        /// </para>
        /// <para>
        /// Expected Output: IOException thrown
        /// </para>
        /// </summary>
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

        // TODO: Add a unit test to test Constructor's algorithm
        //     1) Everything in 1 file
        //     2) Both settings and extensions in another file
    }
}