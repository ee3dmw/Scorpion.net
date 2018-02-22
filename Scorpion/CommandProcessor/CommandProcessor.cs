using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scorpion.CommandProcessor
{
    public class CommandProcessor : IProcessCommands
    {
        readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandProcessor(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        public async Task ProcessAsync<T>(T command)
        {
            var handler = (IHandleCommandsAsync<T>)_commandHandlerFactory.CreateAsync<T>();

            var attributes = GetAttributes<T>(handler);

            RunBeforeAttributes(command, attributes);

            await handler.Handle(command);
            _commandHandlerFactory.Dispose(handler);

            RunPostAttributesAndDispose(command, attributes);
        }

        public void Process<T>(T command)
        {
            var handler = (IHandleCommands<T>)_commandHandlerFactory.Create<T>();

            var attributes = GetAttributes<T>(handler);

            RunBeforeAttributes(command, attributes);

            handler.Handle(command);
            _commandHandlerFactory.Dispose(handler);

            RunPostAttributesAndDispose(command, attributes);
        }

        List<IHandlerAttribute<T>>  GetAttributes<T>(IHandleCommands handler)
        {
            // TODO bad bad performance due to reflection. Probably want to cache all these look ups?
            var customAttributes = new List<IHandlerAttribute<T>>();
            var allAttributes = Attribute.GetCustomAttributes(handler.GetType().GetMethod("Handle"));
            foreach (var attribute in allAttributes)
            {
                var attributeType = attribute.GetType();
                if (attributeType.GetInterfaces().Any(i => i.Name == "IHandleAttribute"))
                {
                    var attributeFromFactory = _commandHandlerFactory.CreateAttribute<T>(attributeType);
                    attributeFromFactory.Order = GetPriority(attribute);
                    customAttributes.Add(attributeFromFactory);
                }
            }

            return customAttributes;
        }

        int GetPriority(Attribute customAttributeData)
        {
            return ((IHandleAttribute) customAttributeData).Order;
        }

        void RunPostAttributesAndDispose<T>(T command, List<IHandlerAttribute<T>> attributes)
        {
            foreach (var attribute in attributes.OrderByDescending(a => a.Order))
            {
                attribute.AfterHandler(command);
                _commandHandlerFactory.Dispose(attribute);
            }
        }

        void RunBeforeAttributes<T>(T command, List<IHandlerAttribute<T>> attributes)
        {
            foreach (var attribute in attributes.OrderBy(a => a.Order))
            {
                attribute.BeforeHandler(command);
            }
        }
    }
}
