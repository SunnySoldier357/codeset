using codeset.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace codesetTest
{
    [TestClass]
    public class FileWrapperTest
    {
        //* Test Methods

        [TestMethod]
        public void ReadExtensionsEmptyTest()
        {
            Dictionary<string, List<string>> result = null;

            try
            {
                result = FileWrapper.ReadExtensions("");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReadExtensionsNullTest()
        {
            Dictionary<string, List<string>> result = null;

            try
            {
                result = FileWrapper.ReadExtensions(null);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReadExtensionsAlgorithmTest()
        {
            string fileName = "ReadExtensionsAlgorithmTest";

            var lines = new string[]
            {
                "",
                "Required",
                "        - item 1",
                "        - item 2",
                "",
                "C#",
                "        - item 3",
                "        - item 4"
            };

            string path = createFile(fileName, lines);

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
                deleteFile(fileName);
            }
        }

        [TestMethod]
        public void ReadExtensionsComplexAlgorithmTest()
        {
            string fileName = "ReadExtensionsComplexAlgorithmTest";

            var lines = new string[]
            {
                "",
                "",
                "    Required",
                "     -    item 1",
                "             -     item 2",
                "",
                "  C#",
                "- item 3",
                "             - item 4"
            };

            string path = createFile(fileName, lines);

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
                deleteFile(fileName);
            }
        }

        //* Private Methods
        private string createFile(string fileName, string[] fileContents)
        {
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(path);

            dir.CreateSubdirectory("Test");

            var directories = dir.GetDirectories();
            var testDir = directories.FirstOrDefault(d => d.Name == "Test");

            OSPlatform os = Utility.GetCurrentOS();
            string divider = "/";
            if (os.Equals(OSPlatform.Windows))
                divider = "\\";

            string filePath = string.Format(
                "{0}{1}{2}.txt", testDir.FullName, divider, fileName);

            FileStream stream = File.Create(filePath);

            using (stream)
            {
                var encoder = new UTF8Encoding(true);

                foreach (string line in fileContents)
                {
                    byte[] lineBytes = encoder.GetBytes(line + "\n");
                    stream.Write(lineBytes, 0, lineBytes.Length);
                }
            }

            return filePath;
        }

        private void deleteFile(string fileName)
        {
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(path);

            OSPlatform os = Utility.GetCurrentOS();
            string divider = "/";
            if (os.Equals(OSPlatform.Windows))
                divider = "\\";

            var directories = dir.GetDirectories();
            var testDir = directories.FirstOrDefault(d => d.Name == "Test");

            File.Delete(string.Format("{0}{1}{2}.txt",
                testDir.FullName, divider, fileName));

            testDir.Delete();
        }
    }
}
