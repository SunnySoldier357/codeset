using System;
using System.Collections.Generic;
using System.Linq;

namespace codeset.Models
{
    public class CodeWrapper
    {
        //* Private Properties
        private TerminalWrapper terminal = new TerminalWrapper();

        //* Public Methods
        public List<string> GetExtensions()
        {
            var extensions = new List<string>();

            string result = terminal.Execute("code --list-extensions");

            foreach (string extension in result.Split('\n'))
                extensions.Add(extension.Trim().ToLower());

            return extensions;
        }

        /// <summary>
        /// Installs the specified extension for VS Code.
        /// </summary>
        /// <param name="extension">
        /// The actual extension id, different from the name.
        /// </param>
        /// <returns>
        /// True if the extension was successfully installed, False otherwise.
        /// </returns>
        public void InstallExtension(string extension)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));
            
            if (string.IsNullOrWhiteSpace(extension))
                throw new ArgumentException(nameof(extension));

            string result = terminal.Execute(string.Format(
                "code --install-extension {0}", extension));;

            // If the string ends in "successfully installed!" or "already installed."
            if (!(result.Substring(result.Length - 23) == "successfully installed!" ||
                result.Substring(result.Length - 18) == "already installed."))
                throw new ArgumentException(nameof(extension));
        }

        public void InstallAllExtensions(ConfigWrapper wrapper)
        {
            if (wrapper == null)
                throw new ArgumentNullException(nameof(wrapper));

            var extensions = wrapper.Extensions;
            // Extension that are going to be installed, not including those
            // that should be installed but are already installed
            var extensionsToInstall = new List<string>();
            // All extensions that should be installed
            var extensionsToKeep = new List<string>();
            var installedExtensions = GetExtensions();

            foreach (var group in extensions)
            {
                if (wrapper.Categories == null ||
                    group.Key == "Required" ||
                    wrapper.Categories.Contains(group.Key))
                {
                    foreach (var extension in group.Value)
                    {
                        string clean = extension.Trim().ToLower();

                        extensionsToKeep.Add(clean);

                        if (!installedExtensions.Contains(clean))
                            extensionsToInstall.Add(clean);
                    }
                }
            }

            var extensionsToUninstall = installedExtensions.Except(extensionsToKeep);

            int i = 1;
            int total = extensionsToUninstall.Count();

            if (total > 0)
            {
                // Uninstall the extensions that do not belong
                Console.WriteLine("\nBeginning to uninstall {0} extension{1} that" +
                " are not in the config file.\n", total, total == 1 ? "" : "s");

                foreach (string extension in extensionsToUninstall)
                {
                    Console.WriteLine("({0}/{1}) Uninstalling {2}...",
                        i++, total, extension);
                    UninstallExtension(extension);
                }

                Console.WriteLine("\nSuccessfully uninstalled {0} extension{1}!\n", total,
                    total == 1 ? "" : "s");
            }

            i = 1;
            total = extensionsToInstall.Count;

            if (total > 0)
            {
                // Install the extensions that do belong
                Console.WriteLine("Beginning to install {0} extension{1}.\n", total,
                    total == 1 ? "" : "s");

                foreach (string extension in extensionsToInstall)
                {
                    Console.WriteLine("({0}/{1}) Installing {2}...",
                        i++, total, extension);
                    InstallExtension(extension);
                }

                Console.WriteLine("\nSuccessfully installed {0} extension{1}!", total,
                    total == 1 ? "" : "s");
            }
            else
                Console.WriteLine("All Required extensions are already installed.");
        }

        public void UninstallExtension(string extension)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            string result = terminal.Execute(string.Format("code --uninstall-extension {0}", extension));
        }

        public void UpdateSettings(ConfigWrapper wrapper)
        {
            
        }
    }
}