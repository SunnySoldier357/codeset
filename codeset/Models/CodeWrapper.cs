using System;
using System.Diagnostics;
using System.Linq;

namespace codeset.Models
{
    public class CodeWrapper
    {
        //* Private Properties
        private Process bashProcess { get; set; }

        //* Constructors
        public CodeWrapper()
        {
            bashProcess = new Process();
            bashProcess.StartInfo.FileName = "bash";
            bashProcess.StartInfo.RedirectStandardInput = true;
            bashProcess.StartInfo.RedirectStandardOutput = true;
            bashProcess.StartInfo.RedirectStandardError = true;
            bashProcess.StartInfo.CreateNoWindow = true;
            bashProcess.StartInfo.UseShellExecute = false;
        }

        ~CodeWrapper()
        {
            bashProcess.WaitForExit();
        }

        //* Public Methods

        /// <summary>
        /// Installs the specified extension for VS Code.
        /// </summary>
        /// <param name="extension">
        /// The actual extension id, different from the name.
        /// </param>
        /// <returns>
        /// True if the extension was successfully installed, False otherwise.
        /// </returns>
        public bool InstallExtension(string extension)
        {
            bashProcess.Start();
            bashProcess.StandardInput.WriteLine("code --install-extension {0}", extension);
            bashProcess.StandardInput.Flush();
            bashProcess.StandardInput.Close();

            string result = bashProcess.StandardOutput.ReadToEnd().Trim();

            // If the string ends in "successfully installed!" or "already installed." return true
            if (result.Substring(result.Length - 23) == "successfully installed!" ||
                result.Substring(result.Length - 18) == "already installed.")
                return true;
            else
                return false;
        }

        public bool InstallAllExtensions(ConfigWrapper wrapper)
        {
            var extensions = wrapper.Extensions;

            int i = 1;
            int total = extensions.Sum(e => e.Value.Count);

            Console.WriteLine("\nBeginning to install {0} extension{1}.\n", total,
                total == 1 ? "" : "s");

            foreach (var group in extensions.Values)
            {
                foreach (var extension in group)
                {
                    Console.WriteLine("({0}/{1}) Installing {2}...",
                        i++, total, extension);
                    InstallExtension(extension);
                }
            }

            Console.WriteLine("\nSuccessfully installed {0} extension{1}!", total,
                total == 1 ? "" : "s");

            return true;
        }
    }
}