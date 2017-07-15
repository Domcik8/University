using PSPuzd2;
using RPG;

namespace DecoratorKingdom
{
    class WizzardDecorator : Decorator
    {
        public WizzardDecorator(Character baseCharacter)
            : base(baseCharacter)
        {
            this.m_Name = "Wizzard";
            this.m_Lifes = -2;
            this.m_Speed = -2;
            this.m_Strength = 3;
            this.m_AttackType = "Fire";
        }

        public string CastSpell()
        {
            if (m_BaseCharacter.GetMana() > 0)
            {
                RPG_Decorator.RPG.PlaySound(@"C:\Users\Dominik\Desktop\VU\5 semestras\Programų sistemų projektavimas\PSPuzd2/FireBall.wav");
                m_BaseCharacter.UseMana();
                return "Spell chanting: Thus the fire came and thee ran\n";
            }
            m_AttackType = "";
            return "(Out of mana)\n";
        }
    }
}
