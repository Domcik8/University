using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
* Plants
*/
namespace FormsOfLife
{
    /**
    * @author Dominik Gabriel Lisovski
    * VU MIF PS6
    */
    public class Plant : FormOfLife
    {
        static int numberOfPlants = 0;
        public int id;
        public int reproductedToday = 0;
   
        public Plant() : this(0) {}
    
        public Plant(int mass) : base(mass)
        {
            numberOfPlants++; id = numberOfPlants;    
        }

        public override void Println() 
        {
            base.Println();
            Console.WriteLine("My age is " + age);
        }

        public override String ToString()
        {
            return(this.GetType().Name + '@' + id);
        }
    
        public override int Grow()
        {
            age++;
            mass++;
            if(age >= 3) return 1;
            return 0;
        }
    }
}
