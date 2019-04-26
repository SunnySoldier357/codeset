using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

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
        /// use the config file at the path to create a Dictionary for the
        /// extensions.
        /// </para>
        /// <para>
        /// config.json:
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

            JObject extensions = createExtensionJObject();

            JObject config = JObject.FromObject(new
            {
                extensions
            });

            string path = CreateFile(fileName, "json",
                config.ToString().Split('\n'));
            
            try
            {
                ConfigWrapper wrapper = new ConfigWrapper(path);

                testExtensions(extensions, wrapper.Extensions);
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
        /// use the config file at the path to create a Dictionary for the
        /// extensions.
        /// </para>
        /// <para>
        /// config.json:
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

            JObject extensions = createExtensionJObject();

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

                testExtensions(extensions, wrapper.Extensions);
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

        /// <summary>
        /// <para>
        /// Tests the algorithm of the Constructor to ensure it can correctly
        /// use the config file at the path to create a Dictionary for the
        /// settings.
        /// </para>
        /// <para>
        /// config.json:
        /// {
        ///     "settings": {
        ///         "Required": [
        ///             {
        ///                 "key": "key 1",
        ///                 "value": "value 1"
        ///             },
        ///             {
        ///                 "key": "key 2",
        ///                 "value": "value 2"
        ///             }
        ///         ],
        ///         "C#": [
        ///             {
        ///                 "key": "key 3",
        ///                 "value": "value 3"
        ///             },
        ///             {
        ///                 "key": "key 3",
        ///                 "value": "value 3"
        ///             }
        ///         ]
        ///     }
        /// }
        /// </para>
        /// <para>
        /// Expected Output: Required and C# as keys and value 1-4 as values in
        /// lists to their respective key (key 1-4)
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorOneFileSettingTest()
        {
            string fileName = "ConstructorOneFileSettingTest";

            JObject settings = createSettingJObject();

            JObject config = JObject.FromObject(new
            {
                settings
            });

            string path = CreateFile(fileName, "json",
                config.ToString().Split('\n'));

            try
            {
                ConfigWrapper wrapper = new ConfigWrapper(path);

                testSettings(settings, wrapper.Settings);
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
        /// use the config file at the path to create a Dictionary for the
        /// settings.
        /// </para>
        /// <para>
        /// Input:
        /// {
        ///     "settings": "path to settings.json"
        /// }
        /// </para>
        /// <para>
        /// settings.json:
        /// {
        ///     "Required": [
        ///         {
        ///             "key": "key 1",
        ///             "value": "value 1"
        ///         },
        ///         {
        ///             "key": "key 2",
        ///             "value": "value 2"
        ///         }
        ///     ],
        ///     "C#": [
        ///         {
        ///             "key": "key 3",
        ///             "value": "value 3"
        ///         },
        ///         {
        ///             "key": "key 3",
        ///             "value": "value 3"
        ///         }
        ///     ]
        /// }
        /// </para>
        /// <para>
        /// Expected Output: Required and C# as keys and value 1-4 as values in
        /// lists to their respective key (key 1-4)
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorDifferentFileSettingTest()
        {
            string configFileName = "config";
            string settingsFileName = "settings";

            JObject settings = createSettingJObject();

            string settingsPath = CreateFile(settingsFileName, "json",
                settings.ToString().Split('\n'));

            JObject config = JObject.FromObject(new
            {
                settings = settingsPath
            });

            string configPath = CreateFile(configFileName, "json",
                config.ToString().Split('\n'));

            try
            {
                ConfigWrapper wrapper = new ConfigWrapper(configPath);

                testSettings(settings, wrapper.Settings);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                DeleteFile(configFileName, "json");
                DeleteFile(settingsFileName, "json");
            }
        }
        
        //* Private Methods
        private JObject createExtensionJObject()
        {
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

            return extensions;
        }

        private JObject createSettingJObject()
        {
            JObject settings = JObject.FromObject(new
            {
                Required = new[]
                {
                    new
                    {
                        key = "key 1",
                        value = "value 1"
                    },
                    new
                    {
                        key = "key 2",
                        value = "value 2"
                    }
                }
            });

            JToken csharp = JToken.FromObject(new[]
            {
                new
                {
                    key = "key 3",
                    value = "value 3"
                },
                new
                {
                    key = "key 4",
                    value = "value 4"
                }
            });

            settings.Add(new JProperty("C#", csharp.ToObject<object>()));

            return settings;
        }

        private void testExtensions(JObject extensions,
            Dictionary<string, List<string>> result)
        {
            string[] keys =
            {
                "Required",
                "C#"
            };

            foreach (string key in keys)
            {
                if (result.ContainsKey(key))
                {
                    var list = result[key];
                    JArray arr = (JArray) extensions[key];

                    for (int i = 0; i < 2; i++)
                        Assert.IsTrue(list[i] == arr[i].ToString());
                }
                else
                    Assert.Fail($"No Key found for '{key}'");
            }
        }

        private void testSettings(JObject settings,
            Dictionary<string, List<Setting>> result)
        {
            string[] keys =
            {
                "Required",
                "C#"
            };

            foreach (string key in keys)
            {
                if (result.ContainsKey(key))
                {
                    var list = result[key];
                    JArray arr = (JArray) settings[key];

                    for (int i = 0; i < 2; i++)
                        Assert.IsTrue(list[i].Equals(new Setting((JObject) arr[i])));
                }
                else
                    Assert.Fail($"No Key found for '{key}'");
            }
        }
    }
}