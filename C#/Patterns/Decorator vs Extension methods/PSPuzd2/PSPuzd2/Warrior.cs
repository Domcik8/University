namespace RPG
{
    class Warrior : Character
    {
        public string Name { get; } = "Warrior";
        public int Lifes { get; } = 5;
        public int Strength { get; } = 3;
        public int Speed { get; } = 3;
        public int Mana { get; set; } = 2;
        public string AttackType { get; } = "Slash";
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
        public override string GetAttackType()
        {
            return AttackType;
        }
        public override int GetMana()
        {
            return Mana;
        }
        public override void UseMana()
        {
            Mana--;
        }
        public override int DeleteRole(string roleName)
        {
            return 1;
        }
        public override Character GetRole(string wizzard)
        {
            return null;
        }
    }
}