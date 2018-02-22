using System;

namespace Scorpion.CommandProcessor
{
    public class Command : ICommand
    {
        public Command()
        {
            CommandId = Guid.NewGuid();
        }

        public Guid CommandId { get; set; }
    }

}