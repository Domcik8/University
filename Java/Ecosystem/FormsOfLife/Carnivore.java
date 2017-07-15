/**
 * Animals eating meat
 */
package FormsOfLife;

import Exception.*;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public class Carnivore extends Animal{
    public Carnivore() throws EcosystemExceptions
    {
        this(0, 0);
    }
    public Carnivore(int mass, int speed){
        super(mass, speed);
    }
    
    public void println(){
        super.println();
        System.out.println("I eat meat");
    }
    public int grow(){
        super.grow();
        //if (mass / 2 == 0) speed++;
        if(age >= 4) return 1;
        return 0;
    }
}
