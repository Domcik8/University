using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
* Animals eating plants
*/
namespace FormsOfLife
{
    /**
    * @author Dominik Gabriel Lisovski
    * VU MIF PS6
    */
    public class Herbivore : Animal
    {
        public Herbivore() : this(0, 0){}
        
        public Herbivore(int mass, int speed) : base(mass, speed){}
    
        public override void Println()
        {
            base.Println();
            Console.WriteLine("I eat plants");
        }

        public override String ToString()
        {
            return(this.GetType().Name + '@' + id);
        }
    
        public override int Grow()
        {
            base.Grow();
            //if (mass / 2 == 0) speed++;
            if(age >= 3) return 1;
            return 0;
        }
    }
}
