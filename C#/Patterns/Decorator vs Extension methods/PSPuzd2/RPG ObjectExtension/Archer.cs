using ExtensionKingdom;

namespace RPG
{
    class Archer : Character
    {
        public string Name { get; } = "Archer";
        public int Lifes { get; } = 3;
        public int Strength { get; } = 5;
        public int Speed { get; } = 5;
        public int Mana { get; set; } = 2;
        public string AttackType { get; } = "Pierce";
        public override string GetName()
        {
            return Name;
        }
        public override int GetLifes()
        {
            return Lifes;
        }
        public override int GetStrenght()
        {
            return Strength;
        }
        public override int GetSpeed()
        {
            return Speed;
        }
        public override int GetMana()
        {
            return Mana;
        }
        public override void UseMana()
        {
            Mana--;
        }
        public override string GetAttackType()
        {
            return AttackType;
        }
    }
}
