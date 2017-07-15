namespace Commands
{
    public interface IUndoableCommand : ICommand
    {
        void Undo();
    }
}
