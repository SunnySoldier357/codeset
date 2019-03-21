using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace codesetTest
{
    public static class Utility
    {
        //* Private Static Properties
        private static OSPlatform? currentOS = null;

        //* Public Static Properties
        public static OSPlatform CurrentOS
        {
            get
            {
                if (currentOS == null)
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        currentOS = OSPlatform.Windows;
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        currentOS = OSPlatform.OSX;
                    else
                        currentOS = OSPlatform.Linux;
                }
                
                return (OSPlatform) currentOS;
            }
        }

        /// <summary>
        /// Creates a file with the specified name and contents in a automatically
        /// created Test folder (deep in Debug folder).
        /// </summary>
        /// <param name="fileName">The name of the file to be created.</param>
        /// <param name="fileExtension">The extension of the file to be created.</param>
        /// <param name="fileContents">The lines of the file.</param>
        /// <returns>
        /// Returns a string that represents the full path to the created file.
        /// </returns>
        public static string CreateFile(string fileName, string fileExtension,
            string[] fileContents)
        {
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(path);

            dir.CreateSubdirectory("Test");

            var directories = dir.GetDirectories();
            DirectoryInfo testDir = directories.FirstOrDefault(d => d.Name == "Test");

            // Make sure of '/' vs '\'
            OSPlatform os = Utility.CurrentOS;
            string divider = "/";
            if (os.Equals(OSPlatform.Windows))
                divider = "\\";

            string filePath = string.Format(
                "{0}{1}{2}.{3}", testDir.FullName, divider, fileName, fileExtension);

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

        /// <summary>
        /// Deletes the  file specified from the automatically created Test
        /// folder (deep in Debug folder).
        /// </summary>
        /// <param name="fileName">The name of the file to be deleted.</param>
        /// <param name="fileExtension">The extension of the file to be deleted.</param>
        public static void DeleteFile(string fileName, string fileExtension)
        {
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(path);

            OSPlatform os = Utility.CurrentOS;
            string divider = "/";
            if (os.Equals(OSPlatform.Windows))
                divider = "\\";

            var directories = dir.GetDirectories();
            var testDir = directories.FirstOrDefault(d => d.Name == "Test");

            File.Delete(string.Format("{0}{1}{2}.{3}",
                testDir.FullName, divider, fileName, fileExtension));

            testDir.Delete();
        }
    }
}