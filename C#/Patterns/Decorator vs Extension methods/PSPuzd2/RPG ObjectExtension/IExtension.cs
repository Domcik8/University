using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG;

namespace ExtensionKingdom
{
    public interface IExtension
    {
        string GetName();
        int GetLifes();
        int GetStrenght();
        int GetSpeed();
        string GetAttackType();
    }
}
