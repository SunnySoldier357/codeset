using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.InteropServices;

using codeset.Models;
using static codeset.Models.Utility;

namespace codesetTest
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
        /// Input: null for lines
        /// </para>
        /// <para>
        /// Expected Output: ArgumentNullException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorNullLinesTest()
        {
            bool caughtException = false;

            try
            {
                Setting setting = new Setting(null);
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
        /// Tests if the Setting class constructor can correctly handle empty value.
        /// </para>
        /// <para>
        /// Input: empty List for lines
        /// </para>
        /// <para>
        /// Expected Output: ArgumentException thrown
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorEmptyLinesTest()
        {
            bool caughtException = false;

            try
            {
                Setting setting = new Setting(null);
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
        public void ConstructorSimpleLinesTest()
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
        public void ConstructorMultiLinesTest()
        {
            JArray value = new JArray(new string[]
            {
                "item1",
                "item2"
            });

            JObject setting = JObject.FromObject(new
            {
                key = "testKey",
                value = value
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
        public void ConstructorOSLinesTest()
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

            if (CurrentOS.Equals(OSPlatform.Linux))
                value = "manjaro";
            else if (CurrentOS.Equals(OSPlatform.OSX))
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
        public void ConstructorInstructionLinesTest()
        {
            JObject setting = JObject.FromObject(new
            {
                key = "testKey",
                value = "testValue",
                instruction = "testInstruction"
            });

            createAndTestSetting(setting, "testKey", null, "testInstruction");
        }

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
                Assert.IsTrue(setting.Value.ToString() == value.ToString(),
                    string.Format("Value - Expected Output: {0} vs Output: {1}",
                        value.ToString(), setting.Value.ToString()));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}