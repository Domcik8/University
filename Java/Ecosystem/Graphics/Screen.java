/**
 * Operates with graphics
 */
package Graphics;

import Ecosystem.World;
import java.awt.Color;
import javax.swing.JButton;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public class Screen extends JPanel{
    
    Color color = Color.green;
    char[][] world;
    int size;
    int day;
    private Image grassImage;
    private Image wolfImage;
    private Image rabbitImage;
    private Image flowerImage;
    private static final int IMG_WIDTH = 85;
    private static final int IMG_HEIGHT = 85;
    public boolean loaded = false;
    int x = 0;
    
    JButton button_Color;
    JButton Wolf;
    JButton Rabbit;
    JButton Flower;
    
    public Screen(char[][] world, int size, int day, final World ecosystem){
        setBackground(color);
        this.world = world;
        this.size = size;
        this.day = day;
        
        setLayout(new FlowLayout());
        button_Color = new JButton("Color");
        button_Color.addActionListener(
            new ActionListener(){
                public void actionPerformed(ActionEvent event){
                    color = JColorChooser.showDialog(null, "Pick your color", color);
                    if(color == null)
                        color = (Color.green);

                    setBackground(color);
                }
            }
        );
        add(button_Color);
        
        Wolf = new JButton("Wolf");
        Wolf.addActionListener(
            new ActionListener(){
                public void actionPerformed(ActionEvent event) {
                    ecosystem.createWolf();
                }
            }
        );
        add(Wolf);
        
        Rabbit = new JButton("Rabbit");
        Rabbit.addActionListener(
            new ActionListener(){
                public void actionPerformed(ActionEvent event) {
                    ecosystem.createRabbit();
                }
            }
        );
        add(Rabbit);
        
        Flower = new JButton("Flower");
        Flower.addActionListener(
            new ActionListener(){
                public void actionPerformed(ActionEvent event) {
                    ecosystem.createFlower();
                }
            }
        );
        add(Flower);
    }
    
    public void paintComponent(Graphics g){
        super.paintComponent(g);
        
        loadImages(this);
    }
    
    public void loadImages(Screen screen){
        grassImage = new ImageIcon("C:\\Users\\Dominik\\Desktop\\VU\\2 semestras\\Objektinis programavimas\\Ecosystem\\Ecosystem 10 Galutine\\Images\\Grass.jpg").getImage();
        wolfImage = new ImageIcon("C:\\Users\\Dominik\\Desktop\\VU\\2 semestras\\Objektinis programavimas\\Ecosystem\\Ecosystem 10 Galutine\\Images\\Wolf.jpg").getImage();
        rabbitImage = new ImageIcon("C:\\Users\\Dominik\\Desktop\\VU\\2 semestras\\Objektinis programavimas\\Ecosystem\\Ecosystem 10 Galutine\\Images\\Rabbit.jpg").getImage();
        flowerImage = new ImageIcon("C:\\Users\\Dominik\\Desktop\\VU\\2 semestras\\Objektinis programavimas\\Ecosystem\\Ecosystem 10 Galutine\\Images\\Flower.jpg").getImage();
        screen.loaded = true;
    }
    
    public void paint(Graphics g){
        int i, j;
        super.paint(g);
        if(loaded){
            setFont(new Font("Arial", Font.PLAIN, 24));
            g.setColor(Color.BLACK);
            if(g instanceof Graphics2D){
                Graphics2D g2 = (Graphics2D)g;
                g2.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_ON);
            }
            printSimpleString("Day: " + day, IMG_WIDTH * size + 30, -15, IMG_HEIGHT * size + 60, g);
            for(i = 0; i < size; i++)
                for(j = 0; j < size; j++)
                    {
                        if(world[i][j] == '.')
                        g.drawImage(grassImage, j * IMG_WIDTH + 5, i * IMG_HEIGHT + 50, null);
                        else if(world[i][j] == 'w')
                        g.drawImage(wolfImage, j * IMG_WIDTH + 5, i * IMG_HEIGHT + 50, null);
                        else if(world[i][j] == 'r')
                        g.drawImage(rabbitImage, j * IMG_WIDTH + 5, i * IMG_HEIGHT + 50, null);
                        else if(world[i][j] == 177)
                        g.drawImage(flowerImage, j * IMG_WIDTH + 5, i * IMG_HEIGHT + 50, null);
                    }
        }
    }
    
    public void setDay(int day){
        this.day = day;
    }
    
    private void printSimpleString(String s, int width, int XPos, int YPos, Graphics g){  
           int stringLen = (int)  
               g.getFontMetrics().getStringBounds(s, g).getWidth();  
           int start = width/2 - stringLen/2;  
           g.drawString(s, start + XPos, YPos);  
    }
}
