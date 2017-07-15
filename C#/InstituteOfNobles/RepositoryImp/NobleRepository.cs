using Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain;

namespace RepositoryImp
{
    public class NobleRepository : INobleRepository
    {
        private int lastNobleId = 1;
        private Dictionary<int, INoble> nobles = new Dictionary<int, INoble>();

        public int Add(INoble noble)
        {
            if (noble.ID == 0)
            {
                noble.ID = lastNobleId++;
            }
            nobles.Add(noble.ID, noble);
            return noble.ID;
        }

        public INoble Get(int nobleId)
        {
            INoble noble;
            nobles.TryGetValue(nobleId, out noble);
            return noble;
        }

        public Dictionary<int, INoble>.ValueCollection GetAll()
        {
            return nobles.Values;
        }

        public void Remove(int id)
        {
            nobles.Remove(id);
        }
    }
}
