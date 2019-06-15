using System;

namespace codeset.Services.Wrappers
{
    public delegate void ExtensionEventHandler(ExtensionEventArgs e);
    
    public class ExtensionEventArgs : EventArgs
    {
        //* Public Properties
        public string ExtensionName { get; set; }

        //* Constructors
        public ExtensionEventArgs(string extensionName) =>
            ExtensionName = extensionName;
    }
}