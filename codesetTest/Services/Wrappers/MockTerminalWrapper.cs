using System;

namespace codeset.Services.Wrappers
{
    public class MockTerminalWrapper : ITerminalWrapper
    {
        //* Events
        public event ExtensionEventHandler InstallCommandExecuted;
        public event ExtensionEventHandler UninstallCommandExecuted;

        //* Public Methods
        public string Execute(string command)
        {
            if (command.Contains("code --install-extension "))
            {
                string extension = command.Replace("code --install-extension ", "");
                InstallCommandExecuted?.Invoke(new ExtensionEventArgs(extension));
            }
            else if (command.Contains("code --uninstall-extension "))
            {
                string extension = command.Replace("code --uninstall-extension ", "");
                UninstallCommandExecuted?.Invoke(new ExtensionEventArgs(extension));
            }

            return null;
        }
    }
}