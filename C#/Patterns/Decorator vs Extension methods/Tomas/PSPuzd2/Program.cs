using System;
using The_Game_Decorator;
using The_Game;

namespace The_Game
{
    class The_Game
    {
        private static void Main(string[] args)
        {
            Intro();
            Console.ReadLine();
            Console.Clear();

            //BaseDragons
            Dragon Xedas = new Dragon("Xedas");
            Dragon Eryzia = new Dragon("Eryzia");
            
            TellTheStory(Xedas);

            Console.ReadLine();
            Console.Clear();

            //Air dragon
            AirElementalDecorator airDragon = new AirElementalDecorator(Xedas);
            
            TellTheStory(airDragon);

            Console.ReadLine();
            Console.Clear();
            
            //Fire dragon
            FireElementalDecorator fireDragon = new FireElementalDecorator(Eryzia);
            
            TellTheStory(fireDragon);

            Console.ReadLine();
            Console.Clear();

            //Air Fire Dragon
            FireElementalDecorator airFireDragon = new FireElementalDecorator(airDragon);
            
            TellTheStory(airFireDragon);

            Console.ReadLine();
            Console.Clear();

            //Delete role
            Console.WriteLine("But not always was he successful... Thus once he has lost his Air element...");
            Console.ReadLine();
            Console.Clear();
            airFireDragon.DeleteRole("Fire");
            
            TellTheStory(airFireDragon);

            Console.ReadLine();
            Console.Clear();
        }

        static void Intro()
        {
            Console.WriteLine("Eaons ago world was a different than it is now.");
            Console.WriteLine("Man was a weak creature and all elements were controlled by mighty dragons.");
        }

        private static void TellTheStory(AbstractDragon dragon)
        {
            string name = dragon.GetName();
            int speed = dragon.GetSpeed();
            int strength = dragon.GetStrenght();
            string element = dragon.GetElement();

            Console.WriteLine(name + " the " + element +"dragon was one of them.");
            Console.WriteLine("Although still being young, he could travel " + speed + " leagues with one swipe of his mighty wings.");
            Console.WriteLine("He was the chosen one, who was foretold to collect all elements into one...\n");

            Console.WriteLine("But for now he has to fight with his hydra brother Ezoeos. \n");

            Console.WriteLine("Tail slash!");
            if (dragon.GetRole("Air") != null)
            {
                Console.Write(((AirElementalDecorator)dragon.GetRole("Air")).WingFling());
            }
            if (dragon.GetRole("Fire") != null)
            {
                Console.Write(((FireElementalDecorator)dragon.GetRole("Fire")).FireBreath());
            }

            Console.WriteLine("Dragon bite!");
            Console.Write("And Ezoeos has lost " + strength + " of his lives.");
        }
    }
}
