using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Ecosystem;
using FormsOfLife;

/**
 * Program simulates virtual environment
*/
namespace WorkTools
{
    /**
     * @author Dominik Gabriel Lisovski
     * VU MIF PS6
     */

    public class Manager
    {
        /**
         *  Main class which controls life of environment;
         */
            
        public static void Main(String[] args)
        {
            int size = 0;
            int perioOfLife = 0;
            int numberOfWolves = 0;
            int numberOfRabbits = 0;
            int numberOfFlowers = 0;
            int n = 0;
            
            System.Console.WriteLine("Enter size of the ecosystem between 4 and 10");
            while (n == 0)
            {
                System.Console.Write("Size : ");
                Int32.TryParse(System.Console.ReadLine(), out size);
                if (size > 3 && size < 11) n = 1;
                else System.Console.WriteLine("Size has to be between 4 and 10");
            }

            n = 0;
            while (n != 3)
            {

                while (n == 0)
                {
                    System.Console.Write("Number of wolves : ");
                    Int32.TryParse(System.Console.ReadLine(), out numberOfWolves);
                    if (numberOfWolves >= 0) n++;
                    else System.Console.WriteLine("Number of wolves should be a natural number");
                }

                while (n == 1)
                {
                    System.Console.Write("Number of rabbits : ");
                    Int32.TryParse(System.Console.ReadLine(), out numberOfRabbits);
                    if (numberOfRabbits >= 0) n++;
                    else System.Console.WriteLine("Number of rabbits should be a natural number");
                }

                while (n == 2)
                {
                    System.Console.Write("Number of flowers : ");
                    Int32.TryParse(System.Console.ReadLine(), out numberOfFlowers);
                    if (numberOfFlowers >= 0) n++;
                    else System.Console.WriteLine("Number of flowers should be a natural number");
                }

                if (size * size < numberOfWolves + numberOfRabbits + numberOfFlowers)
                {
                    n = 0;
                    System.Console.WriteLine("Sum of all forms of life should be not greater than the size * size of the world");
                }
            }

            n = 0;
            while(n == 0)
            {
                System.Console.Write("Enter life period of ecosystem : ");
                Int32.TryParse(System.Console.ReadLine(), out perioOfLife);
                if(perioOfLife >= 0 && perioOfLife <= 50 ) n = 1;
                else System.Console.WriteLine("Life period should be a natural number smaller or equal to 50");
            }


            World ecosystem = new World(size);
            ecosystem.CreateAll(numberOfWolves, numberOfRabbits, numberOfFlowers);
            ecosystem.life(perioOfLife);

            try{
                Thread.Sleep(50);
            }
            catch (ArgumentOutOfRangeException exc)
            {
                System.Console.WriteLine("Error while thread was sleeping");
                System.Console.WriteLine(exc);
            }
            Console.ReadLine();
            
        }
    }
}
