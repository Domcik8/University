namespace The_Game
{
    class Dragon : AbstractDragon
    {
        public string Name { get; }
        public int Lifes { get; } = 2;
        public int Strength { get; } = 2;
        public int Speed { get; } = 2;
        public int WillPower { get; set; } = 1;
        public string Element { get; } = "";
        public Dragon(string name)
        {
            this.Name = name;
        }
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
        public override int GetWillPower()
        {
            return WillPower;
        }
        public override void LooseWillPower()
        {
            WillPower--;
        }
        public override string GetElement()
        {
            return Element;
        }
        public override AbstractDragon DeleteRole(string roleName)
        {
            return null;
        }
        public override AbstractDragon GetRole(string wizzard)
        {
            return null;
        }
    }
}