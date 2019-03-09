using System.Collections.Generic;
using System.IO;

namespace codeset.Models
{
    public class FileWrapper
    {
        // Public Properties
        public Dictionary<string, List<string>> Extensions { get; set; }

        // Public Methods

        /// <summary>
        /// Read in extensions.txt and convert it to a Dictionary
        /// </summary>
        /// <param name="extensionFilePath"></param>
        public static Dictionary<string, List<string>> ReadExtensions(string extensionFilePath)
        {
            var result = new Dictionary<string, List<string>>();


            using (StreamReader stream = new StreamReader(extensionFilePath))
            {
                string line = null;
                string tempHeading = null;
                var tempExtensions = new List<string>();

                while ((line = stream.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.Length > 0)
                    {
                        if (!line.StartsWith('-'))
                        {
                            if (tempHeading != null)
                            {
                                result.Add(tempHeading, tempExtensions);
                                tempExtensions = new List<string>();
                            }

                            // The heading like 'Required'
                            tempHeading = line;
                        }
                        else
                        {
                            // The actual extension
                            tempExtensions.Add(line.Substring(1).Trim());
                        }
                    }
                }

                if (tempHeading != null)
                {
                    result.Add(tempHeading, tempExtensions);
                    tempExtensions = new List<string>();
                }
            }

            return result;
        }
    }
}