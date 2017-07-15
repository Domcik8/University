using System;
using Domain;
using Repository;

namespace Commands
{
    public class CreateInstitute : ICommand
    {
        private Random random = new Random();
        private IDomainFactory domainFactory;
        private IInstituteRepository instituteRepository;
        private IInstituteObserver instituteObserver;
        private string name;

        public CreateInstitute(IDomainFactory domainFactory, IInstituteRepository instituteRepository,
            IInstituteObserver instituteObserver, string name)
        {
            this.domainFactory = domainFactory;
            this.instituteRepository = instituteRepository;
            this.name = name;
            this.instituteObserver = instituteObserver;
        }

        public int Execute()
        {
            IInstitute institute = domainFactory.CreateInstitute(name);
            instituteRepository.Add(institute);
            institute.AddObserver(instituteObserver);
            return 0;
        }
    }
}