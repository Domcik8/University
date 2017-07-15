using System.Collections.Generic;
using Commands;

namespace Controller
{
    public class DefaultCommandProcessor : ICommandProcessor
    {
        private Stack<ICommand> commands = new Stack<ICommand>();

        public int Execute(ICommand command)
        {
            if (0 == command.Execute())
            {
                commands.Push(command);
                return 0;
            }
            return 1;
        }

        public void Undo()
        {
            if((commands.Count > 0) && (commands.Peek() is IUndoableCommand))
            {
                IUndoableCommand command = (IUndoableCommand) commands.Pop();
                command.Undo();
            }
        }
    }
}