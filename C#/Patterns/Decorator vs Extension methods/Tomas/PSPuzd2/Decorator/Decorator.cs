using The_Game;

namespace The_Game
{
    public abstract class Decorator : AbstractDragon
    {
        protected AbstractDragon m_BaseDragon = null;
        
        protected int m_Lifes = 0;
        protected int m_Strength = 0;
        protected int m_Speed = 0;
        protected string m_Element = "";

        protected Decorator(AbstractDragon baseComponent)
        {
            m_BaseDragon = baseComponent;
        }

        public override string GetName()
        {
            return string.Format(m_BaseDragon.GetName());
        }
        public override int GetLifes()
        {
            return m_Lifes + m_BaseDragon.GetLifes();
        }
        public override int GetStrenght()
        {
            return m_Strength + m_BaseDragon.GetStrenght();
        }
        public override int GetSpeed()
        {
            return m_Speed + m_BaseDragon.GetSpeed();
        }
        public override int GetWillPower()
        {
            return m_BaseDragon.GetWillPower();
        }
        public override void LooseWillPower()
        {
            m_BaseDragon.LooseWillPower();
        }
        public override string GetElement()
        {
            if (m_Element != "")
                return string.Format(m_Element + " " + m_BaseDragon.GetElement());
            return string.Format(m_BaseDragon.GetElement());
        }
        public override AbstractDragon DeleteRole(string element)
        {
            if(m_Element == element)
                return m_BaseDragon;
            AbstractDragon role = m_BaseDragon.DeleteRole(element);
            if (role == null)
                return this;
            m_BaseDragon = role;
            return this;
        }

        public override AbstractDragon GetRole(string element)
        {
            if (m_Element == element)
            {
                return this;
            }
            return m_BaseDragon.GetRole(element);
        }



        /*
        public override int UltraDeleteRoleByName(string roleName, AbstractCharacter parentCharacter)
        {
            UltraDeleteRoleByType(GetRole(roleName), this);
        }
        
        public override int UltraDeleteRoleByType(AbstractCharacter delete, AbstractCharacter parentCharacter)
        {
            if(parentCharacter == null && forDeleteCharacter == this)
                return m_BaseCharacter

            else if(forDeleteCharacter == this)

            if (m_Name == roleName)
            {
                return m_BaseCharacter;
            }

            return m_BaseCharacter.GetRole(roleName);
        }
        */
    }
}
