using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Linq;

using codeset.Models;
using codeset.Services;
using codeset.Services.Wrappers;

using codesetTest.Utilities;

namespace codesetTest.Tests.ServicesTest.WrappersTest
{
    [TestClass]
    public class ConfigWrapperTest
    {
        //* Test Methods

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFilePathTest()
        {
            // Arrange
            var platformService = new MockPlatformService(OSPlatform.Linux);
            var settingsService = new MockSettingsService("");

            var configWrapper = new ConfigWrapper(platformService, settingsService);

            // Act
            _ = configWrapper.Categories;
        }

        /// <summary>
        /// <para>
        /// Tests the algorithm of the Constructor to ensure it can correctly
        /// use the config file at the path to create a List for the
        /// categories.
        /// </para>
        /// <para>
        /// config.json:
        /// <code>
        /// {
        ///     "categories": [
        ///         "Category 1",
        ///         "Category 2"
        ///     ]
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: A List with 3 categories - "Required", "Category 1",
        /// "Category 2"
        /// </para>
        /// </summary>
        [TestMethod]
        public void OneFileCategoriesTest()
        {
            // Arrange
            string fileName = "config";

            var categories = new List<string>
            {
                "Category 1",
                "Category 2"
            };

            JObject config = JObject.FromObject(new
            {
                categories
            });

            string path = FileUtility.CreateFile(fileName, FileExtension.Json,
                config.ToString().Split('\n'));

            var platformService = new MockPlatformService(OSPlatform.Linux);
            var settingsService = new MockSettingsService(path);

            var configWrapper = new ConfigWrapper(platformService, settingsService);

            // Act
            var result = configWrapper.Categories;

            // Assert
            var test = result
                .Where(c => c != "Required")
                .ToList();

            CollectionAssert.AreEquivalent(categories, test);
            CollectionAssert.Contains(configWrapper.Categories, "Required");

            // Cleanup
            FileUtility.DeleteFile(fileName, FileExtension.Json);
        }

        /// <summary>
        /// <para>
        /// Tests the algorithm of the Constructor to ensure it can correctly
        /// use the config file at the path to create a Dictionary for the
        /// extensions.
        /// </para>
        /// <para>
        /// config.json:
        /// <code>
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
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: Required and C# as keys and items 1-4 as values in
        /// lists to their respective key
        /// </para>
        /// </summary>
        [TestMethod]
        public void OneFileExtensionTest()
        {
            // Arrange
            string fileName = "config";

            var extensions = createExtensions();

            JObject config = JObject.FromObject(new
            {
                extensions
            });

            string path = FileUtility.CreateFile(fileName, FileExtension.Json,
                config.ToString().Split('\n'));

            var platformService = new MockPlatformService(OSPlatform.Linux);
            var settingsService = new MockSettingsService(path);

            var configWrapper = new ConfigWrapper(platformService, settingsService);

            // Act
            var result = configWrapper.Extensions;

            // Assert
            CollectionAssert.AreEquivalent(extensions.Keys, result.Keys);
            CollectionAssert.AreEquivalent(extensions["Required"], result["Required"]);
            CollectionAssert.AreEquivalent(extensions["C#"], result["C#"]);

            // Cleanup
            FileUtility.DeleteFile(fileName, FileExtension.Json);
        }

        /// <summary>
        /// <para>
        /// Tests the algorithm of the Constructor to ensure it can correctly
        /// turn config file at the path into a Dictionary for the extensions.
        /// </para>
        /// <para>
        /// Input:
        /// <code>
        /// {
        ///     "extensions": "path to extensions.json"
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// extensions.json:
        /// <code>
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
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: Required and C# as keys and items 1-4 as values in
        /// lists to their respective key
        /// </para>
        /// </summary>
        [TestMethod]
        public void DifferentFileExtensionTest()
        {
            // Arrange
            string configFileName = "config";
            string extensionsFileName = "extensions";

            var extensions = createExtensions();

            JObject extensionsJson = JObject.FromObject(extensions);

            string extensionsPath = FileUtility.CreateFile(extensionsFileName,
                FileExtension.Json, extensionsJson.ToString().Split('\n'));

            JObject config = JObject.FromObject(new
            {
                extensions = extensionsPath
            });

            string configPath = FileUtility.CreateFile(configFileName,
                FileExtension.Json, config.ToString().Split('\n'));

            var platformService = new MockPlatformService(OSPlatform.Linux);
            var settingsService = new MockSettingsService(configPath);

            var configWrapper = new ConfigWrapper(platformService, settingsService);

            // Act
            var result = configWrapper.Extensions;

            // Assert
            CollectionAssert.AreEquivalent(extensions.Keys, result.Keys);
            CollectionAssert.AreEquivalent(extensions["Required"], result["Required"]);
            CollectionAssert.AreEquivalent(extensions["C#"], result["C#"]);

            // Cleanup
            FileUtility.DeleteFile(configFileName, FileExtension.Json);
            FileUtility.DeleteFile(extensionsFileName, FileExtension.Json);
        }

        /// <summary>
        /// <para>
        /// Tests the algorithm of the Constructor to ensure it can correctly
        /// use the config file at the path to create a Dictionary for the
        /// settings.
        /// </para>
        /// <para>
        /// config.json:
        /// <code>
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
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: Required and C# as keys and value 1-4 as values in
        /// lists to their respective key (key 1-4)
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorOneFileSettingTest()
        {
            // Arrange
            string fileName = "config";

            JObject settings = createSettingJObject();

            JObject config = JObject.FromObject(new
            {
                settings
            });

            string path = FileUtility.CreateFile(fileName, FileExtension.Json,
                config.ToString().Split('\n'));

            var platformService = new MockPlatformService(OSPlatform.Linux);
            var settingsService = new MockSettingsService(path);

            var configWrapper = new ConfigWrapper(platformService, settingsService);

            // Act
            var result = configWrapper.Settings;

            // Assert
            testSettings(settings, result, platformService);

            // Cleanup
            FileUtility.DeleteFile(fileName, FileExtension.Json);
        }

        /// <summary>
        /// <para>
        /// Tests the algorithm of the Constructor to ensure it can correctly
        /// use the config file at the path to create a Dictionary for the
        /// settings.
        /// </para>
        /// <para>
        /// Input:
        /// <code>
        /// {
        ///     "settings": "path to settings.json"
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// settings.json:
        /// <code>
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
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: Required and C# as keys and value 1-4 as values in
        /// lists to their respective key (key 1-4)
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorDifferentFileSettingTest()
        {
            // Arrange
            string configFileName = "config";
            string settingsFileName = "settings";

            JObject settings = createSettingJObject();

            string settingsPath = FileUtility.CreateFile(settingsFileName,
                FileExtension.Json, settings.ToString().Split('\n'));

            JObject config = JObject.FromObject(new
            {
                settings = settingsPath
            });

            string configPath = FileUtility.CreateFile(configFileName,
                FileExtension.Json, config.ToString().Split('\n'));

            var platformService = new MockPlatformService(OSPlatform.Linux);
            var settingsService = new MockSettingsService(configPath);

            var configWrapper = new ConfigWrapper(platformService, settingsService);

            // Act
            var result = configWrapper.Settings;

            // Assert
            testSettings(settings, result, platformService);

            // Cleanup
            FileUtility.DeleteFile(configFileName, FileExtension.Json);
            FileUtility.DeleteFile(settingsFileName, FileExtension.Json);
        }

        //* Private Methods
        private Dictionary<string, List<string>> createExtensions()
        {
            return new Dictionary<string, List<string>>
            {
                {
                    "Required",
                    new List<string>
                    {
                        "item 1",
                        "item 2"
                    }
                },
                {
                    "C#",
                    new List<string>
                    {
                        "item 3",
                        "item 4"
                    }
                }
            };
        }

        private JObject createSettingJObject()
        {
            return JObject.FromObject(new Dictionary<string, dynamic>
            {
                {
                    "Required",
                    new[]
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
                },
                {
                    "C#",
                    new[]
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
                    }
                }
            });
        }

        private void testSettings(JObject settings,
            Dictionary<string, List<Setting>> result,
            IPlatformService platformService)
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
                        Assert.IsTrue(list[i].Equals(new Setting((JObject) arr[i],
                            platformService)));
                }
                else
                    Assert.Fail($"No Key found for '{key}'");
            }
        }
    }
}