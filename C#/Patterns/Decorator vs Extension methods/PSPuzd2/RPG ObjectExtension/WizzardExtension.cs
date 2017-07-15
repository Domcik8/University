using RPG;

namespace ExtensionKingdom
{
    class WizzardExtension : IExtension
    {
        readonly Character baseCharacter;
        string m_Name = "Wizard";
        int m_Lifes = -2;
        int m_Speed = -2;
        int m_Strength = 3;
        string m_AttackType = "Fire";

        public WizzardExtension(Character baseCharacter)
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
        public string CastSpell()
        {
            if (baseCharacter.GetMana() > 0)
            {
                baseCharacter.UseMana();
                RPG_ObjectExtension.RPG.PlaySound(@"C:\Users\Dominik\Desktop\VU\5 semestras\Programų sistemų projektavimas\PSPuzd2/FireBall.wav");
                return "Spell chanting: Thus the fire came and thee ran\n";
            }
            m_AttackType = "";
            return "(Out of mana)\n";
        }
    }
}
