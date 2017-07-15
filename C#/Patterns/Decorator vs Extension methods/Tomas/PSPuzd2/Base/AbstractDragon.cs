namespace The_Game
{
    public abstract class AbstractDragon
    {
        public abstract string GetName();
        public abstract int GetLifes();
        public abstract int GetStrenght();
        public abstract int GetSpeed();
        public abstract int GetWillPower();
        public abstract void LooseWillPower();
        public abstract string GetElement();
        public abstract AbstractDragon DeleteRole(string roleName);
        public abstract AbstractDragon GetRole(string roleName);
    }
}