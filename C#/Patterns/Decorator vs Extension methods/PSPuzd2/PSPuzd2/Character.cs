namespace RPG
{
    public abstract class Character
    {
        public abstract string GetName();
        public abstract int GetLifes();
        public abstract int GetStrenght();
        public abstract int GetSpeed();
        public abstract int GetMana();
        public abstract void UseMana();
        public abstract string GetAttackType();
        public abstract int DeleteRole(string roleName);
        public abstract Character GetRole(string roleName);
    }
}