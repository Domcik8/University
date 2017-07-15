/**
 * Lets us use graphics
 */
package Graphics;

import javax.swing.JFrame;
import java.awt.*;
import Ecosystem.World;


/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public class Frame {
    
    static final long serialVersionUID = 1L;
    int IMG_WIDTH = 85;
    int IMG_HEIGHT = 85;
    public JFrame frame;
    public Screen screen;
    
    public Frame(char[][] world, int size, int day, World ecosystem){
        frame = new JFrame("Ecosystem");
        
        screen = new Screen(world, size, day, ecosystem);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.add(screen, BorderLayout.CENTER);
        frame.setSize(IMG_WIDTH * size + 5, IMG_HEIGHT * size + 120);
        frame.setVisible(true);
        
        //Set to middle of screen
        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width/2-frame.getSize().width/2, dim.height/2-frame.getSize().height/2);
        
        frame.setResizable(false);
    }
}
