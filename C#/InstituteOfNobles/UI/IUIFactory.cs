using Domain;

namespace UI
{
    public interface IUIFactory
    {
        IUIDialog uidialog { get; }
        INobleObserver nobleObserver { get; }
        IInstituteObserver instituteObserver { get; }
    }
}
