/**
 * At the end of simulation outputs information about final day and number of forms of life to file
 */
package WorkTools;

import javax.swing.JOptionPane;
import java.io.*;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public class Statistics extends Thread{
    int numberOfWolves;
    int numberOfRabbits;
    int numberOfFlowers;
    int data;
    int day;
    
    //Constructor
    public Statistics(int day, int numberOfWolves, int numberOfRabbits, int numberOfFlowers){
    this.day = day;
    this.numberOfWolves = numberOfWolves;
    this.numberOfRabbits = numberOfRabbits;
    this.numberOfFlowers = numberOfFlowers;
    }
    synchronized public void run(boolean information)
    {
        try{
         FileOutputStream fstream = new FileOutputStream("C:\\Users\\Dominik\\Desktop\\VU\\Objektinis programavimas\\Ecosystem 7\\data\\Statistics.txt");
         DataOutputStream out = new DataOutputStream(fstream);
         out.writeInt(day);
         out.writeInt(numberOfWolves);
         out.writeInt(numberOfRabbits);
         out.writeInt(numberOfFlowers);
         out.close();
        }catch (Exception e){
              JOptionPane.showMessageDialog(null, e.getMessage(),
                        "Error!", JOptionPane.ERROR_MESSAGE);
        }
   }
}
