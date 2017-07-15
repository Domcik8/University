/**
 * Program simulates virtual environment
 */
package WorkTools;

import Ecosystem.*;
import javax.swing.JOptionPane;
import javax.swing.JFrame;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public class Manager{
    /**
     *  Main class which controls life of environment;
     */
            
    public static void main(String[] args){
        String input;
        int size = 0;
        int perioOfLife = 0;
        int numberOfWolves = 0;
        int numberOfRabbits = 0;
        int numberOfFlowers = 0;
        int n = 0;
        int statistics = 1;
        
        while(n == 0)
        {
            input = JOptionPane.showInputDialog("Enter size of the ecosystem between 4 and 10"); 
            try { 
                size = Integer.parseInt(input); 
                if(size > 3 && size < 11) n = 1;
                    else JOptionPane.showMessageDialog(null, "Size of ecosystem should be a natural number, between 4 and 10",
                          "Warning!", JOptionPane.WARNING_MESSAGE);
                } 
            catch(Exception ex) {
                JOptionPane.showMessageDialog(null, "Size of ecosystem should be a natural number, smaller than 50 and bigger than 3",
                          "Warning!", JOptionPane.WARNING_MESSAGE);
            }
        }
        n = 0;
        while(n != 3)
        {
            while(n == 0)
            {
                input = JOptionPane.showInputDialog("Number of wolves"); 
                try {  
                    numberOfWolves = Integer.parseInt(input); 
                    if(numberOfWolves >= 0) n++;
                        else JOptionPane.showMessageDialog(null, "Number of wolves should be a natural number",
                          "Warning!", JOptionPane.WARNING_MESSAGE);
                } 
                catch(Exception ex) {
                    JOptionPane.showMessageDialog(null, "Number of wolves should be a natural number",
                          "Warning!", JOptionPane.WARNING_MESSAGE);
                }
            }
            while(n == 1)
            {
                input = JOptionPane.showInputDialog("Number of rabbits"); 
                try {  
                    numberOfRabbits = Integer.parseInt(input); 
                    if(numberOfRabbits >= 0) n++;
                        else JOptionPane.showMessageDialog(null, "Number of rabbits should be a natural number",
                          "Warning!", JOptionPane.WARNING_MESSAGE);
                } 
                catch(Exception ex) {
                    JOptionPane.showMessageDialog(null, "Number of rabbits should be a natural number",
                          "Warning!", JOptionPane.WARNING_MESSAGE);
                }
            }
            while(n == 2)
            {
                input = JOptionPane.showInputDialog("Number of flowers"); 
                try {  
                    numberOfFlowers = Integer.parseInt(input);
                    if(numberOfFlowers >= 0) n++;
                        else JOptionPane.showMessageDialog(null, "Number of flowers should be a natural number",
                          "Warning!", JOptionPane.WARNING_MESSAGE);
                } 
                catch(Exception ex) { 
                    JOptionPane.showMessageDialog(null, "Number of flowers should be a natural number",
                          "Warning!", JOptionPane.WARNING_MESSAGE);
                }
            }
            if(size*size < numberOfWolves + numberOfRabbits + numberOfFlowers)
            {
                n = 0;
                JOptionPane.showMessageDialog(null, "Sum of all forms of life should be not greater than the size*size of the world",
                        "Error!", JOptionPane.ERROR_MESSAGE);
            }
        }
        n = 0;
        while(n == 0)
        {
            input = JOptionPane.showInputDialog("Enter life period of ecosystem"); 
            try { 
                perioOfLife = Integer.parseInt(input); 
                if(perioOfLife >= 0 && perioOfLife <= 50 ) n = 1;
                    else JOptionPane.showMessageDialog(null, "Life period should be a natural number smaller or equal to 50",
                          "Warning!", JOptionPane.WARNING_MESSAGE);
                } 
            catch(Exception ex) {
                JOptionPane.showMessageDialog(null, "Life period should be a natural number smaller or equal to 50",
                          "Warning!", JOptionPane.WARNING_MESSAGE);
            }
        }
        World ecosystem = new World(size);
        ecosystem.createAll(numberOfWolves, numberOfRabbits, numberOfFlowers);
        ecosystem.life(perioOfLife);
        
        try{
            Thread.sleep(50);
        }catch (InterruptedException exc){JOptionPane.showMessageDialog(null, exc.getMessage(),
                               "Error!", JOptionPane.ERROR_MESSAGE);}
        
        /*statistics = JOptionPane.showConfirmDialog(null, "Do you want to get statistics from this program", "Statistics", statistics);
        
        if(statistics == 0)
        {
            try{
            FileInputStream fstream = new FileInputStream("C:\\Users\\Dominik\\Desktop\\VU\\Objektinis programavimas\\Ecosystem 7\\data\\Statistics.txt");
            DataInputStream in = new DataInputStream(fstream);
            System.out.println("Day: " + in.readInt() + " Number of wolves: "+ in.readInt() + " Number of rabbits: " + in.readInt() + " Number of flowers: " +in.readInt());
            in.close();
            fstream.close();
            }catch (Exception e){
                 JOptionPane.showMessageDialog(null, e.getMessage(),
                            "Error!", JOptionPane.ERROR_MESSAGE);
            } 
        }*/
        
        /*World ecosystem = new World(8);
        ecosystem.createAll(4,4,4);
        ecosystem.life(15);*/
    }
}