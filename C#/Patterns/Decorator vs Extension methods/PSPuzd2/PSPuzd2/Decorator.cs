using RPG;

namespace PSPuzd2
{
    public abstract class Decorator : Character
    {
        protected Character m_BaseCharacter = null;

        protected string m_Name = "";
        protected int m_Lifes = 0;
        protected int m_Strength = 0;
        protected int m_Speed = 0;
        protected string m_AttackType = "";

        protected Decorator(Character baseComponent)
        {
            m_BaseCharacter = baseComponent;
        }

        public override string GetName()
        {
            if(m_Name != "")
                return string.Format(m_Name + " " +  m_BaseCharacter.GetName());
            return string.Format(m_BaseCharacter.GetName());
        }
        public override int GetLifes()
        {
            return m_Lifes + m_BaseCharacter.GetLifes();
        }
        public override int GetStrenght()
        {
            return m_Strength + m_BaseCharacter.GetStrenght();
        }
        public override int GetSpeed()
        {
            return m_Speed + m_BaseCharacter.GetSpeed();
        }
        public override int GetMana()
        {
            return m_BaseCharacter.GetMana();
        }
        public override void UseMana()
        {
            m_BaseCharacter.UseMana();
        }
        public override string GetAttackType()
        {
            if (m_AttackType != "")
                return string.Format(m_AttackType + " " + m_BaseCharacter.GetAttackType());
            return string.Format(m_BaseCharacter.GetAttackType());
        }


        public override int DeleteRole(string roleName)
        {
            if(m_Name == roleName)
            {
                m_Name = "";
                m_Lifes = 0;
                m_Strength = 0;
                m_Speed = 0;
                m_AttackType = "";
                return 0;
            }
            return m_BaseCharacter.DeleteRole(roleName);
        }

        public override Character GetRole(string roleName)
        {
            if (m_Name == roleName)
            {
                return this;
            }
            return m_BaseCharacter.GetRole(roleName);
        }



        /*
        public override int UltraDeleteRoleByName(string roleName, Character parentCharacter)
        {
            UltraDeleteRoleByType(GetRole(roleName), this);
        }
        
        public override int UltraDeleteRoleByType(Character delete, Character parentCharacter)
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
