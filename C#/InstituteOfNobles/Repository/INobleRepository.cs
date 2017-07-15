using System.Collections.Generic;
using Domain;

namespace Repository
{
    public interface INobleRepository
    {
        int Add(INoble institute);
        INoble Get(int nobleId);
        Dictionary<int, INoble>.ValueCollection GetAll();
        void Remove(int nobleId);
    }
}
