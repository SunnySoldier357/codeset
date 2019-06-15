using System;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Linq;

using codeset.Models;
using codeset.Services;

namespace codesetTest.Tests.ModelsTest
{
    [TestClass]
    public class SettingTest
    {
        //* Test Methods

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullTest()
        {
            // Arrange
            var platformService = new MockPlatformService(OSPlatform.Linux);

            // Act
            _ = new Setting(null, platformService);
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided.
        /// </para>
        /// <para>
        /// Input:
        /// <code>
        /// {
        ///     "key": "testKey",
        ///     "value": "testValue"
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value: "testValue", Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorSimpleTest()
        {
            // Arrange
            string key = "testkey";
            JToken value = JToken.FromObject("testValue");

            JObject setting = JObject.FromObject(new
            {
                key,
                value
            });

            var platformService = new MockPlatformService(OSPlatform.Linux);

            // Act & Assert
            createAndTestSetting(setting, key, value, null, platformService);
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a multi line
        /// key-value pair value provided.
        /// </para>
        /// <para>
        /// Input:
        /// <code>
        /// {
        ///     "key": "testKey",
        ///     "value": [
        ///         "item1",
        ///         "item2"
        ///     ]
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value: JArray with 2 items (item1
        /// and item2), Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorValueArrayTest()
        {
            // Arrange
            string key = "testKey";
            
            JArray value = JArray.FromObject(new string[]
            {
                "item1",
                "item2"
            });

            JObject setting = JObject.FromObject(new
            {
                key,
                value
            });

            var platformService = new MockPlatformService(OSPlatform.Linux);

            // Act & Assert
            createAndTestSetting(setting, key, value, null, platformService);
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a value that is a
        /// JObject itself.
        /// </para>
        /// <para>
        /// Input:
        /// <code>
        /// {
        ///     "key": "testKey",
        ///     "value": {
        ///         "test": "value 1",
        ///         "another": "value 2"
        ///     }
        /// }
        /// </code>
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
            // Arrange
            string key = "testKey";

            JObject value = JObject.FromObject(new
            {
                test = "value 1",
                another = "value 2"
            });

            JObject setting = JObject.FromObject(new
            {
                key,
                value
            });

            var platformService = new MockPlatformService(OSPlatform.Linux);

            // Act & Assert
            createAndTestSetting(setting, key, value, null, platformService);
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided and return the correct value for the Linux OS.
        /// </para>
        /// <para>
        /// Input:
        /// <code>
        /// {
        ///     "key": "testKey",
        ///     "value": {
        ///         "windows": "windows",
        ///         "osx": "osx",
        ///         "linux": "linux"
        ///     }
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value: "linux", Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorStringValueLinuxTest() =>
            testStringValueForOs(OSPlatform.Linux);

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided and return the correct value for the Mac (OSX) OS.
        /// </para>
        /// <para>
        /// Input:
        /// <code>
        /// {
        ///     "key": "testKey",
        ///     "value": {
        ///         "windows": "windows",
        ///         "osx": "osx",
        ///         "linux": "linux"
        ///     }
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value: "osx", Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorStringValueOsxTest() =>
            testStringValueForOs(OSPlatform.OSX);

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided and return the correct value for the Windows OS.
        /// </para>
        /// <para>
        /// Input:
        /// <code>
        /// {
        ///     "key": "testKey",
        ///     "value": {
        ///         "windows": "windows",
        ///         "osx": "osx",
        ///         "linux": "linux"
        ///     }
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value: "windows", Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorStringValueWindowsTest() =>
            testStringValueForOs(OSPlatform.Windows);

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided and return the instruction.
        /// </para>
        /// <para>
        /// Input:
        /// <code>
        /// {
        ///     "key": "testKey",
        ///     "value": "testValue",
        ///     "instruction": "testInstruction"
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// Expected Output: Key: "testKey", Value: null, Instruction: "testInstruction"
        /// </para>
        /// </summary>
        [TestMethod]
        public void ConstructorInstructionTest()
        {
            // Arrange
            string key = "testKey";
            string instruction = "testInstruction";

            JObject setting = JObject.FromObject(new
            {
                key,
                value = "testValue",
                instruction
            });

            var platformService = new MockPlatformService(OSPlatform.Linux);

            // Act & Assert
            createAndTestSetting(setting, key, null, instruction, platformService);
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
            string instruction, IPlatformService platformService)
        {
            // Act
            Setting setting = new Setting(json, platformService);

            // Assert
            Assert.IsTrue(setting.Instruction == instruction,
                string.Format("Instruction - Expected Output: {0} vs Output: {1}",
                    instruction, setting.Instruction));
            Assert.IsTrue(setting.Key == key,
                string.Format("Key - Expected Output: {0} vs Output: {1}",
                    key, setting.Key));
            
            if (setting.ValueForOS != null && value != null)
                Assert.IsTrue(setting.ValueForOS.ToString() == value.ToString(),
                    string.Format("Value - Expected Output: {0} vs Output: {1}",
                        value.ToString(), setting.ValueForOS.ToString()));
        }

        private void testStringValueForOs(OSPlatform platform)
        {
            // Arrange
            string key = "testKey";

            JObject setting = JObject.FromObject(new
            {
                key,
                value = new
                {
                    windows = "windows",
                    osx = "osx",
                    linux = "linux"
                }
            });

            IPlatformService platformService = new MockPlatformService(platform);

            string value = "windows";

            if (platformService.IsOsLinux())
                value = "linux";
            else if (platformService.IsOsOsx())
                value = "osx";

            JToken valueToken = JToken.FromObject(value);

            // Assert & Act
            createAndTestSetting(setting, key, valueToken, null, platformService);
        }
    }
}