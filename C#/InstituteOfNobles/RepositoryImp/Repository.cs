using Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain;

namespace RepositoryImp
{
    public class NobleRepository : IRepository
    {
        private int lastNobleId = 1;
        private int lastinstituteId = 1;
        private Dictionary<int, INoble> nobles = new Dictionary<int, INoble>();
        private Dictionary<int, IInstitute> institutes = new Dictionary<int, IInstitute>();

        public int AddNoble(INoble noble)
        {
            if (noble.ID == 0)
            {
                noble.ID = lastNobleId++;
            }

            nobles.Add(noble.ID, noble);

            return noble.ID;
        }

        public int AddInstitute(IInstitute institute)
        {
            if (institute.ID == 0)
            {
                institute.ID = lastinstituteId++;
            }

            institutes.Add(institute.ID, institute);

            return institute.ID;
        }

        public INoble GetNoble(int nobleId)
        {
            INoble noble;
            nobles.TryGetValue(nobleId, out noble);
            return noble;
        }

        public I GetInstitute(int insiituteId)
        {
            INoble noble;
            nobles.TryGetValue(studentId, out noble);
            return noble;
        }

        public Dictionary<int, INoble>.ValueCollection GetAll()
        {
            return nobles.Values;
        }

        public void Remove(int studentId)
        {
            nobles.Remove(studentId);
        }
    }
}
