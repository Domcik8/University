using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;

/**
* All forms of life extend this class
*/
namespace FormsOfLife
{
    /**
        * @author Dominik Gabriel Lisovski
        * VU MIF PS6
        */
    public abstract class FormOfLife : Point
    {
        protected int mass;
        public char symbol = '0';
        public int x = 0, y = 0;
        protected int age = 0;
    
        public FormOfLife() : this(0) {}
    
        public FormOfLife(int mass){
            this.mass = mass;
        }

        public virtual void Println()
        {
            Console.WriteLine(ToString());
            Console.WriteLine("My mass is " + mass);
        }

        public override String ToString()
        {
            return("Form of life");
        }

        public abstract int Grow();
        
        public void SetMass(int mass){ 
            this.mass = mass;
        }
    
        public void SetCoordinates(int x, int y){
            this.x = x; this.y = y;
        }
    
        public int GetMass(){ 
            return(mass);
        }
    
        public int GetX(){
            return (x);
        }
    
        public int GetY(){
            return (y);
        }
    }
}




