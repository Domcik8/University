﻿using System;
using The_Game_Extension;
using The_Game;

namespace RPG_ObjectExtension
{
    class RPG
    {
        static void Main(string[] args)
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
            Xedas.AddExtension("Air", new AirElementalExtension(Xedas));

            TellTheStory(Xedas);

            Console.ReadLine();
            Console.Clear();

            //Fire dragon
            Eryzia.AddExtension("Fire", new FireElementExtension(Eryzia));

            TellTheStory(Eryzia);

            Console.ReadLine();
            Console.Clear();

            //Air Fire Dragon
            Xedas.AddExtension("Fire", new FireElementExtension(Xedas));

            TellTheStory(Xedas);

            Console.ReadLine();
            Console.Clear();

            //Delete role
            Console.WriteLine("But not always was he successful... Thus once he has lost his Air element...");
            Console.ReadLine();
            Console.Clear();
            Xedas.DropExtension("Air");

            TellTheStory(Xedas);

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

            foreach (IExtension extension in dragon._extensions.Values)
            {
                if (extension.GetElement() != "")
                {
                    element = extension.GetElement() + " " + element;
                }
                strength += extension.GetStrenght();
                speed += extension.GetSpeed();
            }

            Console.WriteLine(name + " the " + element + "dragon was one of them.");
            Console.WriteLine("Although still being young, he could travel " + speed + " leagues with one swipe of his mighty wings.");
            Console.WriteLine("He was the chosen one, who was foretold to collect all elements into one...\n");

            Console.WriteLine("But for now he has to fight with his hydra brother Ezoeos. \n");

            Console.WriteLine("Tail slash!");
            if (dragon.GetExtension("Air") != null)
            {
                Console.Write(((AirElementalExtension)dragon.GetExtension("Air")).WingFling());
            }
            if (dragon.GetExtension("Fire") != null)
            {
                Console.Write(((FireElementExtension)dragon.GetExtension("Fire")).FireBreath());
            }
            
            Console.WriteLine("Dragon bite!");
            Console.Write("And Ezoeos has lost " + strength + " of his lives.");
        }
    }
}



