using The_Game;

namespace The_Game_Extension
{
    class FireElementExtension : IExtension
    {
        readonly AbstractDragon baseDragon;

        int m_Lifes = 3;
        int m_Speed = 2;
        int m_Strength = 5;
        string m_Element = "Fire";

        public FireElementExtension(AbstractDragon baseDragon)
        {
            this.baseDragon = baseDragon;
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
        public string GetElement()
        {
            return m_Element;
        }
        public string FireBreath()
        {
            if (baseDragon.GetWillPower() > 0)
            {
                baseDragon.LooseWillPower();
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = @"FireBall.wav";
                player.Load();
                player.Play();
                return "*Fire breath*\n";
            }
            return "*Will power is fading this dragon.*\n";
        }
    }
}
