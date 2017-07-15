using ExtensionKingdom;

namespace RPG
{
    public interface ICharacter
    {
       string GetName();
       int GetLifes();
       int GetStrenght();
       int GetSpeed();
       string GetAttackType();
       int GetMana();
       void UseMana();
       void AddExtension(string extensionName, IExtension extension);
       IExtension GetExtension(string name);
       void DropExtension(string ninja);
    }
}