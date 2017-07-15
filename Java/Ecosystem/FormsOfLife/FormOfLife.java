/**
 * All forms of life extend this class
 */
package FormsOfLife;

import Interface.Point;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public abstract class FormOfLife implements Point{
    int mass;
    public char symbol = 0;
    public int x = 0, y = 0;
    int age = 0;
    
    public FormOfLife(){
        this(0);
    }
    
    public FormOfLife(int mass){
        this.mass = mass;
    }
    
    public void println(){
        System.out.println(toString());
        System.out.println("My mass is " + mass);
    }
    
    public String toString(){
        return("Form of life");
    }
        
    public final void setMass(int mass){ 
        this.mass = mass;
    }
    
    public final void setCoordinates(int x, int y){
        this.x = x; this.y = y;
    }
    
    public final int getMass(){ 
        return(mass);
    }
    
    public final int getX(){
        return (x);
    }
    
    public final int getY(){
        return (y);
    }
}
