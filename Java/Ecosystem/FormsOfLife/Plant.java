/**
 * Plants
 */
package FormsOfLife;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public class Plant extends FormOfLife{
    static int numberOfPlants = 0;
    public int id;
    public int reproductedToday = 0;
   
    public Plant(){
        this(0);
    }
    
    public Plant(int mass){
        super(mass);
        numberOfPlants++; id = numberOfPlants;    
    }
    
    public void println(){
        super.println();
        System.out.println("My age is " + age);
    }
    
    public String toString(){
        return(getClass().getName() + '@' + id);
    }
    
    public int grow(){
        age++;
        mass++;
        if(age >= 3) return 1;
        return 0;
    }
}