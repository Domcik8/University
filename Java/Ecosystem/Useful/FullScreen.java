/**
 * Lets us use full screen
 */
package Useful;

import java.awt.*;
import javax.swing.JFrame;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public class FullScreen{
    
    private GraphicsDevice vc;
    
    public FullScreen(){
    GraphicsEnvironment env = GraphicsEnvironment.getLocalGraphicsEnvironment();
    vc = env.getDefaultScreenDevice();
    }
    
    public void setFullScreen(DisplayMode dm, JFrame window){
        window.setUndecorated(true);
        window.setResizable(false);
        vc.setFullScreenWindow(window);
        
        if(dm != null && vc.isDisplayChangeSupported()){
            try{
                vc.setDisplayMode(dm);
            }catch(Exception ex){}
        }
    }
    
    public Window getFullScreenWindow(){
        return vc.getFullScreenWindow();
    }
    
    public void restoreScreen(){
        Window w = vc.getFullScreenWindow();
        if(w != null){
            w.dispose();
        }
        vc.setFullScreenWindow(null);
    }
    
}
