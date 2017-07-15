using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
* Animals eating meat
*/
namespace FormsOfLife
{
    /**
    * @author Dominik Gabriel Lisovski
    * VU MIF PS6
    */
    public class Carnivore : Animal
    {
        public Carnivore() : this(0, 0) {}

        public Carnivore(int mass, int speed) : base(mass, speed){}
    
        public override void Println(){
            base.Println();
            Console.WriteLine("I eat meat");
        }

        public override int Grow(){
            base.Grow();
            //if (mass / 2 == 0) speed++;
            if(age >= 4) return 1;
            return 0;
        }

    }
}
