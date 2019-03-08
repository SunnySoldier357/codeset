using System;
using System.Diagnostics;

namespace codeset.Models
{
    public class CodeWrapper
    {
        //* Public Methods

        public bool InstallExtension(string extension)
        {
            var bashProcess = new Process();
            bashProcess.StartInfo.FileName = "bash";
            bashProcess.StartInfo.RedirectStandardInput = true;
            bashProcess.StartInfo.RedirectStandardOutput = true;
            bashProcess.StartInfo.CreateNoWindow = true;
            bashProcess.StartInfo.UseShellExecute = false;
            bashProcess.Start();

            bashProcess.StandardInput.WriteLine("code --install-extension {0}", extension);
            bashProcess.StandardInput.Flush();
            bashProcess.StandardInput.Close();
            bashProcess.WaitForExit();

            Console.WriteLine(bashProcess.StandardOutput.ReadToEnd());

            return true;
        }
    }
}