using System.Collections.Generic;
using Domain;

namespace Repository
{
    public interface IInstituteRepository
    {
        int Add(IInstitute institute);
        IInstitute Get(int instituteId);
        Dictionary<int, IInstitute>.ValueCollection GetAll();
        void Remove(int instituteId);
    }
}
