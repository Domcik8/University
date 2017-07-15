using The_Game;

namespace The_Game_Decorator
{
    class FireElementalDecorator : Decorator
    {
        public FireElementalDecorator(AbstractDragon baseDragon)
            : base(baseDragon)
        {
            this.m_Lifes = 3;
            this.m_Speed = 2;
            this.m_Strength = 5;
            this.m_Element = "Fire";
        }
        public string FireBreath()
        {
            if (m_BaseDragon.GetWillPower() > 0)
            {
                m_BaseDragon.LooseWillPower();
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
