using System;
using Domain;

namespace DomainImp
{
    public class WarFactory: IDomainFactory
    {
        public INoble CreateNoble(string title, int instituteId)
        {
            return new WarriorNoble(title, instituteId);
        }
        
        public IInstitute CreateInstitute(string name)
        {
            return new InstituteOfWar(name);
        }
    }
}
