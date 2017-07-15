using The_Game;

namespace The_Game_Decorator
{
    class AirElementalDecorator : Decorator
    {
        public AirElementalDecorator(AbstractDragon baseDragon)
            : base(baseDragon)
        {
            this.m_Lifes = 2;
            this.m_Speed = 5;
            this.m_Strength = 2;
            this.m_Element = "Air";
        }

        public string WingFling()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = @"WingFling.wav";
            player.Load();
            player.Play();

            return "*Flings his wings*\n";
        }
    }
}
