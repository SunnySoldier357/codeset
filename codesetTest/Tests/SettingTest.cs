using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

using codeset.Models;
using static codeset.Models.Utility;

namespace codesetTest.Tests
{
    [TestClass]
    public class SettingTest
    {
        //* Test Methods

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can correctly handle null value.
        /// </para>
        /// <para>
        /// Input: null for setting
        /// </para>
        /// <para>
        /// Expected Output: ArgumentNullException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorNullTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new Setting(null));
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided.
        /// </para>
        /// <para>
        /// Input:
        /// {
        ///     "key": "testKey",
        ///     "value": "testValue"
        /// }
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value: "testValue", Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorSimpleTest()
        {
            JObject setting = JObject.FromObject(new
            {
                key = "testKey",
                value = "testValue"
            });

            createAndTestSetting(setting, "testKey",
                JToken.FromObject("testValue"), null);
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a multi line
        /// key-value pair value provided.
        /// </para>
        /// <para>
        /// Input:
        /// {
        ///     "key": "testKey",
        ///     "value": [
        ///         "item1",
        ///         "item2"
        ///     ]
        /// }
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value: JArray with 2 items (item1
        /// and item2), Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorValueArrayTest()
        {
            JArray value = JArray.FromObject(new string[]
            {
                "item1",
                "item2"
            });

            JObject setting = JObject.FromObject(new
            {
                key = "testKey",
                value
            });

            createAndTestSetting(setting, "testKey", value, null);
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a value that is a
        /// JObject itself.
        /// <para>
        /// <para>
        /// Input:
        /// {
        ///     "key": "testKey",
        ///     "value": {
        ///         "test": "value 1",
        ///         "another": "value 2"
        ///     }
        /// }
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value: JObject with 2 keys ("test"
        /// and "another") and their respective values ("value 1" and "value 2"),
        /// Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorValueObjectTest()
        {
            JObject value = JObject.FromObject(new
            {
                test = "value 1",
                another = "value 2"
            });

            JObject setting = JObject.FromObject(new
            {
                key = "testKey",
                value
            });

            createAndTestSetting(setting, "testKey", value, null);
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided and return the correct value for the current OS.
        /// </para>
        /// <para>
        /// Input:
        /// {
        ///     "key": "testKey",
        ///     "value": {
        ///         "windows": "windows",
        ///         "osx": "osx",
        ///         "linux": "manjaro"
        ///     }
        /// }
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value (Windows): "windows", Value (OSX):
        /// "osx", Value (Linux): "manjaro", Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorValueOSTest()
        {
            JObject setting = JObject.FromObject(new
            {
                key = "testKey",
                value = new
                {
                    windows = "windows",
                    osx = "osx",
                    linux = "manjaro"
                }
            });

            string value = "windows";

            if (IsOsLinux())
                value = "manjaro";
            else if (IsOsOsx())
                value = "osx";

            createAndTestSetting(setting, "testKey", JToken.FromObject(value),
                null);
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided and return the instruction.
        /// </para>
        /// <para>
        /// Input:
        /// {
        ///     "key": "testKey",
        ///     "value": "testValue",
        ///     "instruction": "testInstruction"
        /// }
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value: null, Instruction: "testInstruction"
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorInstructionTest()
        {
            JObject setting = JObject.FromObject(new
            {
                key = "testKey",
                value = "testValue",
                instruction = "testInstruction"
            });

            createAndTestSetting(setting, "testKey", null, "testInstruction");
        }

        // TODO: Create a test to test for different OS Values where the value is
        // TODO: a JObject.

        //* Private Methods

        /// <summary>
        /// Creates a Setting instance based on parameter and tests it to the
        /// provided values to ensure they are the same.
        /// </summary>
        /// <param name="json">
        /// The JSON object representing the setting from settings.json
        /// </param>
        /// <param name="key">The right value for Setting's Key property.</param>
        /// <param name="value">The right value for Setting's Value property.</param>
        /// <param name="instruction">
        /// The right value for Setting's Instruction property.
        /// </param>
        private void createAndTestSetting(JObject json, string key, JToken value,
            string instruction)
        {
            try
            {
                Setting setting = new Setting(json);

                Assert.IsTrue(setting.Instruction == instruction,
                    string.Format("Instruction - Expected Output: {0} vs Output: {1}",
                        instruction, setting.Instruction));
                Assert.IsTrue(setting.Key == key,
                    string.Format("Key - Expected Output: {0} vs Output: {1}",
                        key, setting.Key));
                if (setting.ValueForOS == null && value == null)
                    Assert.IsNull(value);
                else
                {
                    Assert.IsTrue(setting.ValueForOS?.ToString() == value?.ToString(),
                        string.Format("Value - Expected Output: {0} vs Output: {1}",
                            value?.ToString(), setting.ValueForOS?.ToString()));
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}