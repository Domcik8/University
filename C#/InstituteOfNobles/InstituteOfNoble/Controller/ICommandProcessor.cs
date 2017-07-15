using Commands;

namespace Controller
{
    public interface ICommandProcessor
    {
        int Execute(ICommand command);
        void Undo();
    }
}
