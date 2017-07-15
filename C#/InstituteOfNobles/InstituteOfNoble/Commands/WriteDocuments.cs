using System;
using System.Collections.Generic;
using Domain;
using Repository;

namespace Commands
{
    public class WriteDocuments : ICommand //IUndoableCommand
    {
        private Dictionary<int, INoble>.ValueCollection nobles;
        private Dictionary<int, IInstitute>.ValueCollection institutes;
        private INobleRepository oldNobleRepository;
        private IInstituteRepository oldInstituteRepository;
        private INobleRepository nobleRepository;
        private IInstituteRepository instituteRepository;
        private Random random = new Random();

        public WriteDocuments(INobleRepository nobleRepository, IInstituteRepository instituteRepository)
        {
            this.nobleRepository = nobleRepository;
            this.instituteRepository = instituteRepository;
        }

        public int Execute()
        {
            nobles = nobleRepository.GetAll();
            oldNobleRepository = nobleRepository;
            institutes = instituteRepository.GetAll();
            oldInstituteRepository = instituteRepository;

            foreach (INoble noble in nobles)
            {
                IInstitute institute = instituteRepository.Get(noble.InstituteId);
                while (institute.Influence < noble.Skill)
                {
                    int oldInfluence = institute.Influence;
                    institute.Influence += institute.Influence / 2;
                    noble.Skill -= oldInfluence;
                    noble.WorksPublished++;
                }
            }
            return 0;
        }

        /*public void Undo()
        {
            nobleRepository = oldNobleRepository;
            instituteRepository = oldInstituteRepository;
        }*/
    }
}