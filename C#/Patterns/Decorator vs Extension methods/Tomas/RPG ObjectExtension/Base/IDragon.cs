using The_Game_Extension;

namespace The_Game
{
    public interface IDragon
    {
        string GetName();
        int GetLifes();
        int GetStrenght();
        int GetSpeed();
        int GetWillPower();
        void LooseWillPower();
        string GetElement();
        void AddExtension(string extensionName, IExtension extension);
        IExtension GetExtension(string name);
        void DropExtension(string ninja);
    }
}