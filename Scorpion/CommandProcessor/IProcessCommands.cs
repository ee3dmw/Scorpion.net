using System.Threading.Tasks;

namespace Scorpion.CommandProcessor
{
    public interface IProcessCommands
    {
        void Process<T>(T command);
        Task ProcessAsync<T>(T command);
    }
}