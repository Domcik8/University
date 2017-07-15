using PSPuzd2;
using RPG;

namespace DecoratorKingdom
{
    class NinjaDecorator : Decorator
    {
        public NinjaDecorator(Character baseCharacter)
            : base(baseCharacter)
        {
            this.m_Name = "Ninja";
            this.m_Lifes = -2;
            this.m_Speed = 2;
            this.m_Strength = 2;
            this.m_AttackType = "Silent";
        }
        public string Sneak()
        {
            return "...\n";
        }
    }
}
