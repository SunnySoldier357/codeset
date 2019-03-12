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

        [TestMethod]
        public void NullLinesTest() =>
            createAndTestSetting(null, null, null, null);

        [TestMethod]
        public void EmptyLinesTest() =>
            createAndTestSetting(new List<string>(), null, null, null);

        [TestMethod]
        public void SimpleLinesTest()
        {
            var lines = new List<string>
            {
                "\"key\": \"value\","
            };

            createAndTestSetting(lines, null, "\"key\"", "\"value\"");
        }

        [TestMethod]
        public void ComplexLinesTest()
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