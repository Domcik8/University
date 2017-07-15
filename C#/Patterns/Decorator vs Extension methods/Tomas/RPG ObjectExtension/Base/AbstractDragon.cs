using System.Collections.Generic;
using System.Linq;
using The_Game_Extension;

namespace The_Game
{
    public abstract class AbstractDragon : IDragon
    {
        public Dictionary<string, IExtension> _extensions = new Dictionary<string, IExtension>();
        public abstract string GetName();
        public abstract int GetLifes();
        public abstract int GetStrenght();
        public abstract int GetSpeed();
        public abstract int GetWillPower();
        public abstract void LooseWillPower();
        public abstract string GetElement();
        
        public void AddExtension(string name, IExtension extension)
        {
            _extensions.Add(name, extension);
        }
        public IExtension GetExtension(string name)
        {
            return (from extension in _extensions where extension.Key.Equals(name) select extension.Value).FirstOrDefault();
        }
        public void DropExtension(string name)
        {
            _extensions.Remove(name);
        }
    }
}