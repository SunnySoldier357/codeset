using System.Collections.Generic;

using codeset.Models;

namespace codeset.Services.Wrappers
{
    public interface IConfigWrapper
    {
        //* Interface Properties
        Dictionary<string, List<string>> Extensions { get; }
        Dictionary<string, List<Setting>> Settings { get; }

        List<string> Categories { get; }
    }
}