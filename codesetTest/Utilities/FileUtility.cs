using System;
using System.IO;
using System.Text;

namespace codesetTest.Utilities
{
    public static class FileUtility
    {
        //* Constants
        public const string DIR_NAME = "Test";

        //* Public Properties
        public static string RootPath => Directory.GetCurrentDirectory();

        //* Public Methods

        /// <summary>
        /// Creates a file with the specified name and contents in a automatically
        /// created Test folder (deep in Debug folder).
        /// </summary>
        /// <param name="fileName">The name of the file to be created.</param>
        /// <param name="fileExtension">The extension of the file to be created.</param>
        /// <param name="fileContents">The lines of the file.</param>
        /// <returns>
        /// Returns a string that represents the relative path to the created file.
        /// </returns>
        public static string CreateFile(string fileName, FileExtension fileExtension,
            string[] fileContents)
        {
            DirectoryInfo dir = new DirectoryInfo(RootPath);
            DirectoryInfo testDir = dir.CreateSubdirectory(DIR_NAME);

            string fullFileName = string.Format("{0}.{1}", fileName,
                fileExtension.ToString().ToLower());

            FileInfo file = new FileInfo(Path.Combine(dir.FullName, fullFileName));
            FileStream stream = file.Create();

            using (stream)
            {
                var encoder = new UTF8Encoding(true);

                foreach (string line in fileContents)
                {
                    byte[] lineBytes = encoder.GetBytes(line + "\n");
                    stream.Write(lineBytes, 0, lineBytes.Length);
                }
            }

            return file.FullName;
        }

        /// <summary>
        /// Deletes the file specified from the automatically created Test
        /// folder (deep in Debug folder).
        /// </summary>
        /// <param name="fileName">The name of the file to be deleted.</param>
        /// <param name="fileExtension">The extension of the file to be deleted.</param>
        public static void DeleteFile(string fileName, FileExtension fileExtension)
        {
            string fullFileName = string.Format("{0}.{1}", fileName,
                fileExtension.ToString().ToLower());
            DirectoryInfo testDir = new DirectoryInfo(RootPath);
            FileInfo file = new FileInfo(Path.Combine(RootPath, fullFileName));

            if (file.Exists)
                file.Delete();

            if (testDir.GetFiles().Length == 0 &&
                testDir.GetDirectories().Length == 0)
                testDir.Delete();
        }

        public static string ReadFile(string fileName, FileExtension fileExtension)
        {
            string fullFileName = string.Format("{0}.{1}", fileName,
                fileExtension.ToString().ToLower());
            DirectoryInfo testDir = new DirectoryInfo(RootPath);
            FileInfo file = new FileInfo(Path.Combine(RootPath, fullFileName));

            if (file.Exists)
            {
                using (StreamReader streamReader = new StreamReader(file.OpenRead()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            else
                return null;
        }
    }
}