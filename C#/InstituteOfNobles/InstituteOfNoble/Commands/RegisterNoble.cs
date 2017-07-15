using Domain;
using Repository;

namespace Commands
{
    public class RegisterNoble : IUndoableCommand
    {
        private IDomainFactory domainFactory;
        private IInstituteRepository instituteRepository;
        private INobleRepository nobleRepository;
        private INobleObserver nobleObserver;
        private string title;
        private int instituteId;
        private int nobleId;

        public RegisterNoble(IDomainFactory domainFactory, IInstituteRepository instituteRepository,
            INobleRepository nobleRepository, INobleObserver nobleObserver, string title, int instituteId)
        {
            this.domainFactory = domainFactory;
            this.instituteRepository = instituteRepository;
            this.nobleRepository = nobleRepository;
            this.nobleObserver = nobleObserver;
            this.title = title;
            this.instituteId = instituteId;
        }

        public int Execute()
        {
            IInstitute institute = instituteRepository.Get(instituteId);
            if (institute != null)
            {
                INoble noble = domainFactory.CreateNoble(title, institute.ID);
                nobleRepository.Add(noble);
                noble.AddObserver(nobleObserver);
                nobleId = noble.ID;
                return 0;
            }
            return 1;
        }

        public void Undo()
        {
            nobleRepository.Remove(nobleId);
        }
    }
}