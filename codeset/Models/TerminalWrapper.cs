using System.Diagnostics;

namespace codeset.Models
{
    public class TerminalWrapper
    {
        //* Private Properties
        private Process process;

        //* Constructor
        public TerminalWrapper()
        {
            process = new Process();
            process.StartInfo.FileName = Utility.IsOsWindows() ? "cmd" : "bash";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
        }

        //* Destructor
        ~TerminalWrapper() =>
            process.WaitForExit();

        //* Public Methods
        public string Execute(string command)
        {
            process.Start();
            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
            process.StandardInput.Close();

            return process.StandardOutput.ReadToEnd().Trim();
        }
    }
}