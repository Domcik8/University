using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
* All animals extend this class
*/
namespace FormsOfLife
{
    /**
    * @author Dominik Gabriel Lisovski
    * VU MIF PS6
    */
    public class Animal : FormOfLife
    {
        /**
        * Carnifore and Herbifore extend this class
        */
        static int numberOfAnimals = 0;
        protected int id;
        public int speed;
        public int moveToday;
    
        public Animal() : this(0, 0) {}
    
        public Animal(int mass, int speed) : base(mass)
        {
            this.speed = speed;
            numberOfAnimals++; id = numberOfAnimals;
            moveToday = speed;
        }
    
        public override void Println()
        {
            base.Println();
            Console.WriteLine("My speed is " + speed);
            Console.WriteLine("My age is " + age);
        }
    
        public override String ToString()
        {
            return(this.GetType().Name + '@' + id);
        }
    
        public void SetSpeed(int speed)
        { 
            this.speed = speed;
        }
    
        public int GetSpeed()
        { 
            return (speed);
        }

        public override int Grow()
        {
            mass++;
            age++;
            return 0;
        }
    }
}
