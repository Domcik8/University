using Domain;
using UI;

namespace UIImp
{
    public class UIFactory : IUIFactory
    {
        public IUIDialog uidialog { get; }
        public INobleObserver nobleObserver { get; }
        public IInstituteObserver instituteObserver { get; }

        public UIFactory(IUIDialog uidialog, INobleObserver nobleObserver, IInstituteObserver instituteObserver)
        {
            this.uidialog = uidialog;
            this.nobleObserver = nobleObserver;
            this.instituteObserver = instituteObserver;
        }
    }
}
