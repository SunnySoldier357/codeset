using System;
using System.Diagnostics;

namespace codeset.Services.Wrappers
{
    public interface ITerminalWrapper
    {
        //* Interface Methods
        string Execute(string command);
    }
    
    /// <summary>
    /// A wrapper for the terminal to execute commands and return outputs as
    /// strings.
    /// </summary>
    public class TerminalWrapper : ITerminalWrapper
    {
        //* Private Properties
        private readonly IPlatformService platformService;

        private Process process;

        //* Constructor
        public TerminalWrapper(IPlatformService platformService)
        {
            this.platformService = platformService;

            process = new Process();
            process.StartInfo.FileName = this.platformService.IsOsWindows() ? "cmd" : "bash";
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
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            process.Start();
            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
            process.StandardInput.Close();

            return process.StandardOutput.ReadToEnd().Trim();
        }
    }
}