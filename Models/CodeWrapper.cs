using System;
using System.Diagnostics;

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
            bashProcess.StartInfo.CreateNoWindow = true;
            bashProcess.StartInfo.UseShellExecute = false;
        }

        ~CodeWrapper()
        {
            bashProcess.WaitForExit();
        }

        //* Public Methods

        public bool InstallExtension(string extension)
        {
            bashProcess.Start();
            bashProcess.StandardInput.WriteLine("code --install-extension {0}", extension);
            bashProcess.StandardInput.Flush();
            bashProcess.StandardInput.Close();

            Console.WriteLine(bashProcess.StandardOutput.ReadToEnd());

            return true;
        }
    }
}