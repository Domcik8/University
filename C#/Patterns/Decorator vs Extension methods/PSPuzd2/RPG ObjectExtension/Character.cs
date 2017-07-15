using System.Collections.Generic;
using System.Linq;
using ExtensionKingdom;

namespace RPG
{
    public abstract class Character : ICharacter
    {
        public Dictionary<string, IExtension> _extensions = new Dictionary<string, IExtension>();
        public abstract string GetName();
        public abstract int GetLifes();
        public abstract int GetStrenght();
        public abstract int GetSpeed();
        public abstract int GetMana();
        public abstract void UseMana();
        public abstract string GetAttackType();
        public void AddExtension(string name, IExtension extension)
        {
            _extensions.Add(name, extension);
        }
        public IExtension GetExtension(string name)
        {
            return (from extension in _extensions where extension.Key.Equals(name) select extension.Value).FirstOrDefault();
        }
        public void DropExtension(string ninja)
        {
            _extensions.Remove(ninja);
        }
    }
}