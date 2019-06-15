using System.Collections.Generic;

using codeset.Models;

namespace codeset.Services.Wrappers
{
    public class MockConfigWrapper : IConfigWrapper
    {
        //* Public Properties
        public Dictionary<string, List<string>> Extensions { get; set; }
        public Dictionary<string, List<Setting>> Settings { get; set; }

        public List<string> Categories { get; set; }

        //* Constructors
        public MockConfigWrapper()
        {
            Extensions = new Dictionary<string, List<string>>();
            Extensions["Required"] = new List<string>();
            
            Settings = new Dictionary<string, List<Setting>>();

            Categories = new List<string>
            {
                "Required"
            };
        }
    }
}