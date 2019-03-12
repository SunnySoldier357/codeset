using codeset.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
        /// Expected Output: null for all properties of Setting
        /// </para>
        /// </summary>
        [TestMethod]
        public void NullLinesTest() =>
            createAndTestSetting(null, null, null, null);

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can correctly handle empty value.
        /// </para>
        /// <para>
        /// Input: empty List for lines
        /// </para>
        /// <para>
        /// Expected Output: null for all properties of Setting
        /// </para>
        /// </summary>
        [TestMethod]
        public void EmptyLinesTest() =>
            createAndTestSetting(new List<string>(), null, null, null);

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided.
        /// </para>
        /// <para>
        /// Input: "key": "value"
        /// </para>
        /// <para>
        /// Expected Output: Key: "key", Value: "value", Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void SimpleLinesTest()
        {
            var lines = new List<string>
            {
                "\"key\": \"value\","
            };

            createAndTestSetting(lines, null, "\"key\"", "\"value\"");
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a multi line
        /// key-value pair value provided.
        /// </para>
        /// <para>
        /// Input: "key": [item1, item2] (over multiple lines)
        /// </para>
        /// <para>
        /// Expected Output: Key: "key", Value: "[\n    item1\n    item2\n]",
        /// Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void MultiLinesTest()
        {
            var lines = new List<string>
            {
                "\"key\": [",
                "    item1",
                "    item2",
                "],"
            };

            string value = "[";
            value += "\n    item1";
            value += "\n    item2";
            value += "\n]";

            createAndTestSetting(lines, null, "\"key\"", value);
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided and return the correct value for the current OS.
        /// </para>
        /// <para>
        /// Input: "key": "Windows:windows;OSX:osx;Linux:manjaro"
        /// </para>
        /// <para>
        /// Expected Output: Key: "key", Value (Windows): "windows", Value (OSX):
        /// "osx", Value (Linux): "manjaro", Instruction: null
        /// </para>
        /// </summary>
        [TestMethod]
        public void OSLinesTest()
        {
            var lines = new List<string>
            {
                "\"key\": \"Windows:windows;OSX:osx;Linux:manjaro\","
            };

            OSPlatform os = Utility.GetCurrentOS();

            string value = "windows";

            if (os.Equals(OSPlatform.Linux))
                value = "manjaro";
            else if (os.Equals(OSPlatform.OSX))
                value = "osx";

            createAndTestSetting(lines, null, "\"key\"",
                string.Format("\"{0}\"", value));
        }

        /// <summary>
        /// <para>
        /// Tests if the Setting class constructor can handle a simple key-value
        /// pair value provided and return the instruction.
        /// </para>
        /// <para>
        /// Input: // TODO: Path to bash executable
        /// "\"key\": \"value\","
        /// </para>
        /// <para>
        /// Expected Output: Key: "key", Value: null,
        /// Instruction: "Path to bash executable"
        /// </para>
        /// </summary>
        [TestMethod]
        public void InstructionLinesTest()
        {
            var lines = new List<string>
            {
                "// TODO: Path to bash executable",
                "\"key\": \"value\","
            };

            createAndTestSetting(lines, "Path to bash executable", "\"key\"", null);
        }

        //* Private Methods

        /// <summary>
        /// Creates a Setting instance based on parameter and tests it to the
        /// provided values to ensure they are the same.
        /// </summary>
        /// <param name="lines">
        /// The lines representing the setting from settings.json
        /// </param>
        /// <param name="instruction">
        /// The right value for Setting's Instruction property.
        /// </param>
        /// <param name="key">The right value for Setting's Key property.</param>
        /// <param name="value">The right value for Setting's Value property.</param>
        private void createAndTestSetting(List<string> lines, string instruction,
            string key, string value)
        {
            Setting setting = new Setting(lines);

            Assert.IsTrue(setting.Instruction == instruction);
            Assert.IsTrue(setting.Key == key);
            Assert.IsTrue(setting.Value == value);
        }
    }
}