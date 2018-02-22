using System;
using Scorpion.IoC;

namespace Scorpion.CommandProcessor
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public IHandleCommands Create<T>()
        {
            try
            {
                var container = Container.Resolve<IHandleCommands<T>>();
                return (IHandleCommands) container;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Can not find handler for type " + typeof(T).FullName + ". Inner Exception=" + ex.Message);
            }
        }

        public IHandleCommands CreateAsync<T>()
        {
            try
            {
                var container = Container.Resolve<IHandleCommandsAsync<T>>();
                return (IHandleCommands)container;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Can not find handler for type " + typeof(T).FullName + ". Inner Exception=" + ex.Message);
            }
        }

        public IHandlerAttribute<T> CreateAttribute<T>(Type type)
        {
            try
            {
                var attribute = (IHandlerAttribute<T>)Container.Resolve(type);
                return attribute;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Can not find attribute for type " + type.FullName + ". Inner Exception=" + ex.Message);
            }
        }

        public void Dispose(object thingToDispose)
        {
            Container.Dispose(thingToDispose);
        }

        
    }
}