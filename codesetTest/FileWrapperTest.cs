using codeset.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using static codesetTest.Utility;

namespace codesetTest
{
    [TestClass]
    public class FileWrapperTest
    {
        //* Test Methods

        /// <summary>
        /// <para>
        /// Tests if the ReadExtension() method can correctly handle null path.
        /// </para>
        /// <para>
        /// Input: null for path
        /// </para>
        /// <para>
        /// Expected Output: ArgumentNullException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void ReadExtensionsNullTest()
        {
            bool caughtException = false;

            try
            {
                FileWrapper.ReadExtensions(null);
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
        /// Tests if the ReadExtension() method can correctly handle empty path.
        /// </para>
        /// <para>
        /// Input: "" for path
        /// </para>
        /// <para>
        /// Expected Output: IOException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void ReadExtensionsEmptyTest()
        {
            bool caughtException = false;

            try
            {
                FileWrapper.ReadExtensions("");
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

        /// <summary>
        /// <para>
        /// Tests the algorithm of the ReadExtensions() method to ensure it can
        /// correctly turn file at the path into a Dictionary.
        /// </para>
        /// <para>
        /// Input: Auto-generated file formatted nicely
        /// </para>
        /// <para>
        /// Expected Output: Required and C# as keys and items 1-4 as values in
        /// lists to their respective key
        /// </para>
        /// </summary>
        [TestMethod]
        public void ReadExtensionsAlgorithmTest()
        {
            string fileName = "ReadExtensionsAlgorithmTest";

            var lines = new string[]
            {
                "{",
                "    \"Required\": [",
                "        \"item 1\",",
                "        \"item 2\",",
                "    ],",
                "    \"C#\": [",
                "        \"item 3\",",
                "        \"item 4\",",
                "    ],",
                "}"
            };

            string path = CreateFile(fileName, "json", lines);

            try
            {
                var result = FileWrapper.ReadExtensions(path);

                if (result.ContainsKey("Required"))
                {
                    var list = result["Required"];

                    Assert.IsTrue(list[0] == "item 1");
                    Assert.IsTrue(list[1] == "item 2");
                }
                else
                    Assert.Fail("No Key found for 'Required'");

                if (result.ContainsKey("C#"))
                {
                    var list = result["C#"];

                    Assert.IsTrue(list[0] == "item 3");
                    Assert.IsTrue(list[1] == "item 4");
                }
                else
                    Assert.Fail("No Key found for 'C#'");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                DeleteFile(fileName, "json");
            }
        }

        /// <summary>
        /// <para>
        /// Tests the algorithm of the ReadExtensions() method to ensure it can
        /// correctly turn the file at the path into a Dictionary.
        /// </para>
        /// <para>
        /// Input: Auto-generated file formatted randomly but using the correct
        /// conventions
        /// </para>
        /// <para>
        /// Expected Output: Required and C# as keys and items 1-4 as values in
        /// lists to their respective key
        /// </para>
        /// </summary>
        [TestMethod]
        public void ReadExtensionsComplexAlgorithmTest()
        {
            string fileName = "ReadExtensionsComplexAlgorithmTest";

            var lines = new string[]
            {
                "",
                "",
                " {",
                "       \"Required\": [",
                "           \"item 1\",",
                "                  \"item 2\",",
                "   ],",
                " \"C#\": [",
                "  \"item 3\",",
                "                \"item 4\",",
                "                     ],",
                "}"
            };

            string path = CreateFile(fileName, "json", lines);

            try
            {
                var result = FileWrapper.ReadExtensions(path);

                if (result.ContainsKey("Required"))
                {
                    var list = result["Required"];

                    Assert.IsTrue(list[0] == "item 1");
                    Assert.IsTrue(list[1] == "item 2");
                }
                else
                    Assert.Fail("No Key found for 'Required'");

                if (result.ContainsKey("C#"))
                {
                    var list = result["C#"];

                    Assert.IsTrue(list[0] == "item 3");
                    Assert.IsTrue(list[1] == "item 4");
                }
                else
                    Assert.Fail("No Key found for 'C#'");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                DeleteFile(fileName, "json");
            }
        }

        /// <summary>
        /// <para>
        /// Tests if the ReadSettings() method can correctly handle null path.
        /// </para>
        /// <para>
        /// Input: null for path
        /// </para>
        /// <para>
        /// Expected Output: ArgumentNullException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void ReadSettingsNullTest()
        {
            bool caughtException = false;

            try
            {
                FileWrapper.ReadSettings(null);
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
        /// Tests if the ReadSettings() method can correctly handle empty path.
        /// </para>
        /// <para>
        /// Input: "" for path
        /// </para>
        /// <para>
        /// Expected Output: IOException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void ReadSettingsEmptyTest()
        {
            bool caughtException = false;

            try
            {
                FileWrapper.ReadSettings("");
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