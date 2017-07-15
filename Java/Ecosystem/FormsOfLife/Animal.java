/**
 * All animals extend this class
 */
package FormsOfLife;

import Exception.*;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public abstract class Animal extends FormOfLife{
    
    /**
     * Carnifore and Herbifore extend this class
     */
    static int numberOfAnimals = 0;
    int id;
    public int speed;
    public int moveToday;
    
    public Animal() throws EcosystemExceptions{
        this(0, 0);
    }
    
    public Animal(int mass, int speed){
        super(mass);
        this.speed = speed;
        numberOfAnimals++; id = numberOfAnimals;
        moveToday = speed;
    }
    
    public void println(){
        super.println();
        System.out.println("My speed is " + speed);
        System.out.println("My age is " + age);
    }
    
    public String toString(){
        return(getClass().getName() + '@' + id);
    }
    
    public final void setSpeed(int speed){ 
        this.speed = speed;
    }
    
    public final int getSpeed(){ 
        return (speed);
    }
    
    public int grow(){
        mass++;
        age++;
        return 0;
    }
}
