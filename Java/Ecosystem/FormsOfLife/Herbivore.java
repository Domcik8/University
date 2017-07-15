/**
 * Animals eating plants
 */
package FormsOfLife;

import Exception.*;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public class Herbivore extends Animal{
    public Herbivore() throws EcosystemExceptions
    {
        this(0, 0);
    }
        
    public Herbivore(int mass, int speed){
        super(mass, speed);
    }
    
    public void println(){
        super.println();
        System.out.println("I eat plants");
    }
    
    public String toString(){
        return(getClass().getName() + '@' + id);
    }
    
    public void speak(){
        System.out.println(" (I need to find some plants)");
    }
    public int grow(){
        super.grow();
        //if (mass / 2 == 0) speed++;
        if(age >= 3) return 1;
        return 0;
    }
}