using System;
using DecoratorKingdom;
using RPG;

namespace RPG_Decorator
{
    class RPG
    {
        private static void Main(string[] args)
        {
            //Warrior
            Warrior warrior = new Warrior();

            Move(warrior);
            Attack(warrior,"Wolf");

            Console.ReadLine();
            Console.Clear();

            //Archer
            Archer archer = new Archer();

            Move(archer);
            Attack(archer, "Wolf");

            Console.ReadLine();
            Console.Clear();

            //Wizzard warrior
            WizzardDecorator wizzard = new WizzardDecorator(warrior);

            Move(wizzard);
            Attack(wizzard, "Wolf");

            Console.ReadLine();
            Console.Clear();

            //Ninja archer
            NinjaDecorator ninja = new NinjaDecorator(archer);

            Move(ninja);
            Attack(ninja, "Wolf");

            Console.ReadLine();
            Console.Clear();

            //Ninja wizzard warrior
            NinjaDecorator ninjaWizzard = new NinjaDecorator(wizzard);

            Move(ninjaWizzard);
            Attack(ninjaWizzard, "Wolf");

            Console.ReadLine();
            Console.Clear();

            //Is he a wizzard?
            Console.WriteLine("Is he a wizzard?");
            Console.ReadLine();
            Console.Clear();
            var maybeWizzard = ninjaWizzard.GetRole("Wizzard");
            if (maybeWizzard != null)
            {
                Move(maybeWizzard);
                Attack(maybeWizzard, "Wolf");
            }
            else Console.WriteLine("Nop");

            Console.ReadLine();
            Console.Clear();

            //Delete role
            Console.WriteLine("Delete that wizzard!");
            Console.ReadLine();
            Console.Clear();
            ninjaWizzard.DeleteRole("Wizzard");

            Move(ninjaWizzard);
            Attack(ninjaWizzard, "Wolf");

            Console.ReadLine();
            Console.Clear();
        }
        static void Move(Character character)
        {
            string name = character.GetName();
            int speed = character.GetSpeed();
            Console.WriteLine("And thus " + name + " has traveled for " + speed + " leagues.");
        }
        private static void Attack(Character character, string enemy)
        {
            string name = character.GetName();
            int strength = character.GetStrenght();
            
            Console.WriteLine("But that shall be the end of his jorney.");
            Console.WriteLine("Behold " + name + ", a mighty " + enemy + " appears before You!");
            Console.WriteLine("And thus a fight began, not for life, but for death.");
            
            if (character.GetRole("Ninja") != null)
            {
                Console.Write(((NinjaDecorator)character.GetRole("Ninja")).Sneak());
            }
            if (character.GetRole("Wizzard") != null)
            {
                Console.Write(((WizzardDecorator)character.GetRole("Wizzard")).CastSpell());
            }

            string attackType = character.GetAttackType();
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
