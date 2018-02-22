using System.Threading.Tasks;

namespace Scorpion.CommandProcessor
{
    public interface IHandleCommands
    { }

    public interface IHandleCommands<T> : IHandleCommands
    {
        void Handle(T command);
    }

    public interface IHandleCommandsAsync<T> : IHandleCommands
    {
        Task Handle(T command);
    }
}