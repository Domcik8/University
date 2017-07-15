using System;
using Domain;

namespace UIImp
{
    public class InstituteChangesReporter : IInstituteObserver
    {
        public void Notify(IInstitute institute)
        {
            Console.WriteLine(institute.ID + ". institute of " + institute.Name + ", influence: " + institute.Influence + "\n");
        }
    }
}
