using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG;

namespace ExtensionKingdom
{
    class NinjaExtension : IExtension
    {
        readonly Character baseCharacter;
        string m_Name = "Ninja";
        int m_Lifes = -2;
        int m_Speed = 2;
        int m_Strength = 2;
        string m_AttackType = "Silent";

        public NinjaExtension(Character baseCharacter)
        {
            this.baseCharacter = baseCharacter;
        }

        public string GetName()
        {
            return m_Name;
        }

        public int GetLifes()
        {
            return m_Lifes;
        }

        public int GetStrenght()
        {
            return m_Strength;
        }

        public int GetSpeed()
        {
            return m_Speed;
        }
        public string GetAttackType()
        {
            return m_AttackType;
        }
        public string Sneak()
        {
            return "...\n";
        }
    }
}
