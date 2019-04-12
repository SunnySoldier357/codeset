using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;

using codeset.Models;
using static codesetTest.Utility;

namespace codesetTest
{
    [TestClass]
    public class ConfigWrapperTest
    {
        //* Test Methods

        /// <summary>
        /// <para>
        /// Tests if the Constructor can correctly handle empty path.
        /// </para>
        /// <para>
        /// Input: "" for path
        /// </para>
        /// <para>
        /// Expected Output: ArgumentException thrown
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

        /// <summary>
        /// <para>
        /// Tests the algorithm of the Constructor to ensure it can correctly
        /// turn config file at the path into a Dictionary for the extensions.
        /// </para>
        /// <para>
        /// Input:
        /// {
        ///     "extensions": {
        ///         "Required": [
        ///             "item 1",
        ///             "item 2"
        ///         ],
        ///         "C#": [
        ///             "item 3",
        ///             "item 4"
        ///         ]
        ///     }
        /// }
        /// </para>
        /// <para>
        /// Expected Output: Required and C# as keys and items 1-4 as values in
        /// lists to their respective key
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorOneFileExtensionTest()
        {
            string fileName = "ConstructorOneFileExtensionTest";

            JObject extensions = JObject.FromObject(new
            {
                Required = new string[]
                {
                    "item 1",
                    "item 2"
                }
            });

            extensions.Add(new JProperty("C#", new string[]
            {
                "item 3",
                "item 4"
            }));

            JObject config = JObject.FromObject(new
            {
                extensions
            });

            string path = CreateFile(fileName, "json",
                config.ToString().Split('\n'));
            
            try
            {
                ConfigWrapper wrapper = new ConfigWrapper(path);
                var result = wrapper.Extensions;

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
        /// Tests the algorithm of the Constructor to ensure it can correctly
        /// turn config file at the path into a Dictionary for the extensions.
        /// </para>
        /// <para>
        /// Input:
        /// {
        ///     "extensions": "path to extensions.json"
        /// }
        /// </para>
        /// <para>
        /// extensions.json:
        /// {
        ///     "Required": [
        ///         "item 1",
        ///         "item 2"
        ///     ],
        ///     "C#": [
        ///         "item 3",
        ///         "item 4"
        ///     ]
        /// }
        /// </para>
        /// <para>
        /// Expected Output: Required and C# as keys and items 1-4 as values in
        /// lists to their respective key
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorDifferentFileExtensionTest()
        {
            string configFileName = "config";
            string extensionsFileName = "extensions";

            JObject extensions = JObject.FromObject(new
            {
                Required = new string[]
                {
                    "item 1",
                    "item 2"
                }
            });

            extensions.Add(new JProperty("C#", new string[]
            {
                "item 3",
                "item 4"
            }));
            
            string extensionsPath = CreateFile(extensionsFileName, "json",
                extensions.ToString().Split('\n'));

            JObject config = JObject.FromObject(new
            {
                extensions = extensionsPath
            });

            string configPath = CreateFile(configFileName, "json",
                config.ToString().Split('\n'));
            
            try
            {
                ConfigWrapper wrapper = new ConfigWrapper(configPath);
                var result = wrapper.Extensions;

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
                DeleteFile(configFileName, "json");
                DeleteFile(extensionsFileName, "json");
            }
        }
    }
}