using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionKingdom;
using RPG;

namespace RPG_ObjectExtension
{
    class RPG
    {
        static void Main(string[] args)
        {
            //Warrior
            Warrior warrior = new Warrior();

            Move(warrior);
            Attack(warrior, "Wolf");

            Console.ReadLine();
            Console.Clear();

            //Archer
            Archer archer = new Archer();

            Move(archer);
            Attack(archer, "Wolf");

            Console.ReadLine();
            Console.Clear();

            //Wizzard warrior
            warrior.AddExtension("Wizzard", new WizzardExtension(warrior));

            Move(warrior);
            Attack(warrior, "Wolf");


            Console.ReadLine();
            Console.Clear();

            //Ninja archer
            archer.AddExtension("Ninja", new NinjaExtension(archer));

            Move(archer);
            Attack(archer, "Wolf");

            Console.ReadLine();
            Console.Clear();

            //Ninja wizzard warrior
            warrior.AddExtension("Ninja", new NinjaExtension(warrior));

            Move(warrior);
            Attack(warrior, "Wolf");

            Console.ReadLine();
            Console.Clear();

            //Delete role
            Console.WriteLine("Delete that ninja!");
            Console.ReadLine();
            Console.Clear();
            //Delete extension
            warrior.DropExtension("Ninja");

            Move(warrior);
            Attack(warrior, "Wolf");

            Console.ReadLine();
            Console.Clear();
        }
        static void Move(Character character)
        {
            string name = character.GetName();
            int speed = character.GetSpeed();

            foreach (IExtension extension in character._extensions.Values)
            {
                name = extension.GetName() + " " + name;
                speed += extension.GetSpeed();
            }

            Console.WriteLine("And thus " + name + " has traveled for " + speed + " leagues.");
        }
        private static void Attack(Character character, string enemy)
        {
            string name = character.GetName();
            string attackType = character.GetAttackType();
            int strength = character.GetStrenght();

            Console.WriteLine("But that shall be the end of his jorney.");
            Console.WriteLine("Behold " + name + ", a mighty " + enemy + " appears before You!");
            Console.WriteLine("And thus a fight began, not for life, but for death.");

            if (character.GetExtension("Ninja") != null)
            {
                Console.Write(((NinjaExtension)character.GetExtension("Ninja")).Sneak());
            }
            if (character.GetExtension("Wizzard") != null)
            {
                Console.Write(((WizzardExtension)character.GetExtension("Wizzard")).CastSpell());
            }

            foreach (IExtension extension in character._extensions.Values)
            {
                name = extension.GetName() + " " + name;
                if (extension.GetAttackType() != "")
                {
                    attackType = extension.GetAttackType() + " " + attackType;
                }
                strength += extension.GetStrenght();
            }
            
            Console.WriteLine(attackType + "\n" + attackType);
            Console.WriteLine("And the wolf has lost " + strength + " of his lives.");
        }

        public static void PlaySound(string path)
        {
            System.Media.SoundPlayer player =
                new System.Media.SoundPlayer();
            player.SoundLocation = path;
            player.Load();
            player.Play();
        }
    }
}
