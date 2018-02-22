using System;

namespace Scorpion.CommandProcessor
{
    public interface ICommandHandlerFactory
    {
        IHandleCommands Create<T>();
        IHandlerAttribute<T> CreateAttribute<T>(Type getType);
        void Dispose(Object thingToDispose);
        IHandleCommands CreateAsync<T>();
    }
}