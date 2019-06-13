using codeset.Models;

namespace codeset.Services
{
    public interface ICommandService
    {
        //* Interface Methods
        int HandleCommand(Options options);
    }
}