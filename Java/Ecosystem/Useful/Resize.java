/**
 * Changes image size and saves it
 */
package Useful;

import java.awt.AlphaComposite;
import java.awt.Graphics2D;
import java.awt.RenderingHints;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import javax.imageio.ImageIO;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public class Resize {
    static int IMG_WIDTH, IMG_HEIGHT;
    
    public Resize(int IMG_WIDTH, int IMG_HEIGHT){
        this.IMG_WIDTH = IMG_WIDTH;
        this.IMG_HEIGHT = IMG_HEIGHT;
    }
    
    public void resizeImages(){
    int i;
    String from = "", to = "";
    try{

        for(i = 0; i < 4; i++)
        {
            if(i == 0) 
            {
                from = "C:\\Users\\Dominik\\Desktop\\VU\\Objektinis programavimas\\Ecosystem 10 Galutine\\Images\\Wolf_original.png";
                to = "C:\\Users\\Dominik\\Desktop\\VU\\Objektinis programavimas\\Ecosystem 10 Galutine\\Images\\Wolf.jpg";
            }
            else if(i == 1) 
            {
                from = "C:\\Users\\Dominik\\Desktop\\VU\\Objektinis programavimas\\Ecosystem 10 Galutine\\Images\\Rabbit_original.png";
                to = "C:\\Users\\Dominik\\Desktop\\VU\\Objektinis programavimas\\Ecosystem 10 Galutine\\Images\\Rabbit.jpg";
            }
            else if(i == 2) 
            {
                from = "C:\\Users\\Dominik\\Desktop\\VU\\Objektinis programavimas\\Ecosystem 10 Galutine\\Images\\Flower_original.png";
                to = "C:\\Users\\Dominik\\Desktop\\VU\\Objektinis programavimas\\Ecosystem 10 Galutine\\Images\\Flower.jpg";
            }
            else if(i == 3) 
            {
                from = "C:\\Users\\Dominik\\Desktop\\VU\\Objektinis programavimas\\Ecosystem 10 Galutine\\Images\\Grass_original.png";
                to = "C:\\Users\\Dominik\\Desktop\\VU\\Objektinis programavimas\\Ecosystem 10 Galutine\\Images\\Grass.jpg";
            }

            BufferedImage originalImage = ImageIO.read(new File(from));
            int type = originalImage.getType() == 0? BufferedImage.TYPE_INT_ARGB : originalImage.getType();

            BufferedImage resizeImageHintPng = resizeImageWithHint(originalImage, type);
            ImageIO.write(resizeImageHintPng, "png", new File(to)); 
        }
        }catch(IOException e){
                System.out.println(e.getMessage());
        }
    }
    
    private static BufferedImage resizeImage(BufferedImage originalImage, int type){
	BufferedImage resizedImage = new BufferedImage(IMG_WIDTH, IMG_HEIGHT, type);
	Graphics2D g = resizedImage.createGraphics();
	g.drawImage(originalImage, 0, 0, IMG_WIDTH, IMG_HEIGHT, null);
	g.dispose();
	return resizedImage;
    }
    
    private static BufferedImage resizeImageWithHint(BufferedImage originalImage, int type){
 
	BufferedImage resizedImage = new BufferedImage(IMG_WIDTH, IMG_HEIGHT, type);
	Graphics2D g = resizedImage.createGraphics();
	g.drawImage(originalImage, 0, 0, IMG_WIDTH, IMG_HEIGHT, null);
	g.dispose();	
	g.setComposite(AlphaComposite.Src);
 
	g.setRenderingHint(RenderingHints.KEY_INTERPOLATION,
	RenderingHints.VALUE_INTERPOLATION_BILINEAR);
	g.setRenderingHint(RenderingHints.KEY_RENDERING,
	RenderingHints.VALUE_RENDER_QUALITY);
	g.setRenderingHint(RenderingHints.KEY_ANTIALIASING,
	RenderingHints.VALUE_ANTIALIAS_ON);
 
	return resizedImage;
    }
}
