using The_Game;

namespace The_Game_Extension
{
    class AirElementalExtension : IExtension
    {
        readonly AbstractDragon baseDragon;
        int m_Lifes = 2;
        int m_Speed = 5;
        int m_Strength = 2;
        string m_Element = "Air";

        public AirElementalExtension(AbstractDragon baseDragon)
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