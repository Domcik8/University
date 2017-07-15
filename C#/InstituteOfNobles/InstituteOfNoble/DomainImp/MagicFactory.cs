using System;
using Domain;

namespace DomainImp
{
    public class MagicFactory : IDomainFactory
    {
        public INoble CreateNoble(string title, int instituteId)
        {
            return new MageNoble(title, instituteId);
        }

        public IInstitute CreateInstitute(string name)
        {
            return new InstituteOfMagic(name);
        }
    }
}