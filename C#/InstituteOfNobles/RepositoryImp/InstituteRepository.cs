using Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain;

namespace RepositoryImp
{
    public class InstituteRepository : IInstituteRepository
    {
        private int lastinstituteId = 1;
        private Dictionary<int, IInstitute> institutes = new Dictionary<int, IInstitute>();
       
        public int Add(IInstitute institute)
        {
            if (institute.ID == 0)
            {
                institute.ID = lastinstituteId++;
            }

            institutes.Add(institute.ID, institute);

            return institute.ID;
        }

        public IInstitute Get(int instituteId)
        {
            IInstitute institute;
            institutes.TryGetValue(instituteId, out institute);
            return institute;
        }

        public Dictionary<int, IInstitute>.ValueCollection GetAll()
        {
            return institutes.Values;
        }

        public void Remove(int instituteId)
        {
            institutes.Remove(instituteId);
        }
    }
}
