/**
 * Ecosystem life simulation
 */
package Ecosystem;

import java.util.Random;
import FormsOfLife.*;
import java.util.*;
import Graphics.Frame;

/**
 * @author Dominik Gabriel Lisovski
 * VU MIF PS6
 */
public class World{
    Random randomGenerator = new Random();
    int xClosest = 0, yClosest = 0;
    int size = 0;
    char[][] world = new char[size][size];
    ArrayList<Carnivore> wolf = new ArrayList<Carnivore>();
    ArrayList<Herbivore> rabbit = new ArrayList<Herbivore>();
    ArrayList<Plant> flower = new ArrayList<Plant>();
    //List list = Collections.synchronizedList(new ArrayList(...));
    int day = 0;
    boolean possible = false;
        
    public World(int size){
        setSize(size);
        cleanWorld();
    }
    
    final void setSize(int size){
        this.size = size;
        world = new char[size][size];
    }
    
    final void cleanWorld(){
        int i; int j;
        for(i = 0; i < size; i++)
            for(j = 0; j < size; j++)
                world[i][j] = '.';
    }
    
    public void createAll(int numberOfWolves, int numberOfRabbits, int numberOfFlowers){
        int i;
        for(i = 0; i < numberOfWolves; i++) createWolf();
        for(i = 0; i < numberOfRabbits; i++) createRabbit();
        for(i = 0; i < numberOfFlowers; i++) createFlower();
    }
    
    public void println(){
        System.out.println("Day: " + day);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++) 
                System.out.print(world[i][j] + "  ");
            System.out.println();
        }
    }
    
    synchronized public void life(int periodOfLife){
        int i, j;
        int found = 0;
        boolean dead;
        Carnivore prototypeOfWolf;
        Herbivore prototypeOfRabbit;
        Plant prototypeOfFlower;
        
        //Graphics
        Frame frame = new Frame(world, size, day, this);
        
        for (i = 0; i < periodOfLife; i++)
        {
            try{
                Thread.sleep(500);
            }catch (Exception e){};
            println();
            //Wolfs live
            for(j = 0; j < wolf.size(); j++)
            {
                dead = false;
                prototypeOfWolf = wolf.get(j);
                if(prototypeOfWolf.moveToday != 0)
                {
                    if(prototypeOfWolf.grow() == 1)
                    {
                             death(prototypeOfWolf);
                             dead = true;
                    }
                    else
                    {
                        found = findClosestFormOfLife(prototypeOfWolf);
                        if(found == 0) moveToClosest(prototypeOfWolf);
                    }
                    if(!dead) 
                        if(checkIllness(prototypeOfWolf)) 
                            if(prototypeOfWolf.grow() == 1)
                            {
                                 death(prototypeOfWolf);
                                 dead = true;
                            }
                    if(found == 0 || dead) 
                        {
                           println();
                           frame.frame.repaint();
                           try{
                              Thread.sleep(500);
                           }catch (Exception e){};
                        }
                }
                if(!dead) prototypeOfWolf.moveToday = prototypeOfWolf.speed;
             }
             //println();
             //Rabbits live
             for(j = 0; j < rabbit.size(); j++)
             {
                dead = false;
                prototypeOfRabbit = rabbit.get(j);
                if(prototypeOfRabbit.moveToday != 0)
                 {
                     if(prototypeOfRabbit.grow() == 1)
                     {
                          death(prototypeOfRabbit);
                          dead = true;
                     }
                 else
                    {
                        found = findClosestFormOfLife(prototypeOfRabbit);
                        if(found == 0) moveToClosest(prototypeOfRabbit);
                    }
                    if(!dead) 
                        if(checkIllness(prototypeOfRabbit)) 
                            if(prototypeOfRabbit.grow() == 1)
                            {
                                 death(prototypeOfRabbit);
                                 dead = true;
                            }
                    if(found == 0 || dead) 
                    {
                       println();
                       frame.frame.repaint();
                       try{
                          Thread.sleep(500);
                       }catch (Exception e){};
                    }
                }
                if(!dead) prototypeOfRabbit.moveToday = prototypeOfRabbit.speed;
             }
             //println();
             //Flowers live
             for(j = 0; j < flower.size(); j++)
             {
                dead = false;
                prototypeOfFlower = flower.get(j);
                if(prototypeOfFlower.reproductedToday == 0)
                     if(prototypeOfFlower.grow() == 1)
                     {
                          death(prototypeOfFlower);
                          dead = true;
                     }
                     else if(!checkIllness(prototypeOfFlower)) 
                         prototypeOfFlower.reproductedToday = reproduction(prototypeOfFlower);
                if(prototypeOfFlower.reproductedToday != 0 || dead)
                {
                     println();
                     frame.frame.repaint();
                     try{
                        Thread.sleep(500);
                     }catch (Exception e){};
                }
                if (!dead) prototypeOfFlower.reproductedToday = 0;
             }
             day++;
             println();
             //new Statistics(day, wolf.size(), rabbit.size(), flower.size()).start();
             frame.screen.setDay(day);
        }
    }
    
    int findClosestFormOfLife(Animal seeker){
        int i = 0;
        double dystans = 0;
        double minDystans = size * size;
        int closestId = -1;
        if(seeker instanceof Carnivore || seeker.symbol == 'w')
        {
            for(Herbivore checked: rabbit) 
            {
                dystans = checkDystans(seeker, checked);
                if(minDystans > dystans) 
                {
                    minDystans = dystans;
                    closestId = i;
                }
                i++;
            }
            if(closestId == -1) return 1;
            xClosest = rabbit.get(closestId).x;
            yClosest = rabbit.get(closestId).y;
        }
        if(seeker instanceof Herbivore || seeker.symbol == 'r')
        {
            for(Plant checked: flower) 
            {
               dystans = checkDystans(seeker, checked);
               if(minDystans > dystans) 
               {
                    minDystans = dystans;
                    closestId = i;
               }
               i++;
            }
            if(closestId == -1) return 1;
            xClosest = flower.get(closestId).x;
            yClosest = flower.get(closestId).y;
        }  
        return 0;
    }
    
    double checkDystans(Animal seeker, FormOfLife target){
        return (Math.hypot((double)(target.x-seeker.x), (double)(target.y-seeker.y)));
    }
    
    void moveToClosest(Animal seeker){
        int i, j = 0;
        int movable = 0;
        
        //PRIDETI ISTRIZAINE Jei nori
        
        if(seeker.x != xClosest && seeker.moveToday != 0)
        {
            j = seeker.moveToday;
            for(i = 0; i < j; i++)
                if(seeker.x < xClosest)
                   {
                       if(world[seeker.x + 1][seeker.y] == 'r' && seeker.symbol == 'w') 
                       {
                           seeker.moveToday = 1;
                           consume(seeker);
                           movable = 1;
                       }
                       else if(world[seeker.x + 1][seeker.y] == 177 && seeker.symbol == 'r') 
                       {
                           seeker.moveToday = 1;
                           consume(seeker);
                           movable = 1;
                       }
                       else if(world[seeker.x + 1][seeker.y] == 177 && seeker.symbol == 'w')
                       {
                           seeker.moveToday = 1;
                           world[seeker.x][seeker.y] = '.';
                           destroy(seeker.x + 1, seeker.y);
                           movable = 1;
                       }
                       else if(world[seeker.x + 1][seeker.y] == '.')
                       {
                           world[seeker.x][seeker.y] = '.';
                           movable = 1;
                       }
                       if(movable == 1)
                       {
                           seeker.moveToday--;
                           world[seeker.x + 1][seeker.y] = seeker.symbol;
                           seeker.x++;
                           movable = 0;
                       }
                   }
            j = seeker.moveToday;
            for(i = 0; i < j; i++)
                if(seeker.x > xClosest)
                {
                       if(world[seeker.x - 1][seeker.y] == 'r' && seeker.symbol == 'w') 
                       {
                           seeker.moveToday = 1;
                           consume(seeker);
                           movable = 1;
                       }
                       else if(world[seeker.x - 1][seeker.y] == 177 && seeker.symbol == 'r') 
                       {
                           seeker.moveToday = 1;
                           consume(seeker);
                           movable = 1;
                       }
                       else if(world[seeker.x - 1][seeker.y] == 177 && seeker.symbol == 'w')
                       {
                           seeker.moveToday = 1;
                           world[seeker.x][seeker.y] = '.';
                           destroy(seeker.x - 1, seeker.y);
                           movable = 1;
                       }
                       else if(world[seeker.x - 1][seeker.y] == '.')
                       {
                           world[seeker.x][seeker.y] = '.';
                           movable = 1;
                       }
                       if(movable == 1)
                       {
                           seeker.moveToday--;
                           world[seeker.x - 1][seeker.y] = seeker.symbol;
                           seeker.x--;
                           movable = 0;
                       }
                }
        }
        if(seeker.y != yClosest && seeker.moveToday != 0)
        {
            j = seeker.moveToday;
            for(i = 0; i < j; i++)
                if(seeker.y < yClosest)
                {
                       if(world[seeker.x][seeker.y + 1] == 'r' && seeker.symbol == 'w') 
                       {
                           seeker.moveToday = 1;
                           consume(seeker);
                           movable = 1;
                       }
                       else if(world[seeker.x][seeker.y + 1] == 177 && seeker.symbol == 'r') 
                       {
                           seeker.moveToday = 1;
                           consume(seeker);
                           movable = 1;
                       }
                       else if(world[seeker.x][seeker.y + 1] == 177 && seeker.symbol == 'w')
                       {
                           seeker.moveToday = 1;
                           world[seeker.x][seeker.y] = '.';
                           destroy(seeker.x, seeker.y + 1);
                           movable = 1;
                       }
                       else if(world[seeker.x][seeker.y + 1] == '.')
                       {
                           world[seeker.x][seeker.y] = '.';
                           movable = 1;
                       }
                       if(movable == 1)
                       {
                           seeker.moveToday--;
                           world[seeker.x][seeker.y + 1] = seeker.symbol;
                           seeker.y++;
                           movable = 0;
                       }
                }
            j = seeker.moveToday;
            for(i = 0; i < j; i++)
                if(seeker.y > yClosest)
                {
                       if(world[seeker.x][seeker.y - 1] == 'r' && seeker.symbol == 'w') 
                       {
                           seeker.moveToday = 1;
                           consume(seeker);
                           movable = 1;
                       }
                       else if(world[seeker.x][seeker.y - 1] == 177 && seeker.symbol == 'r') 
                       {
                           seeker.moveToday = 1;
                           consume(seeker);
                           movable = 1;
                       }
                       else if(world[seeker.x][seeker.y - 1] == 177 && seeker.symbol == 'w')
                       {
                           seeker.moveToday = 1;
                           world[seeker.x][seeker.y] = '.';
                           destroy(seeker.x, seeker.y - 1);
                           movable = 1;
                       }
                       else if(world[seeker.x][seeker.y - 1] == '.')
                       {
                           world[seeker.x][seeker.y] = '.';
                           movable = 1;
                       }
                       if(movable == 1)
                       {
                           seeker.moveToday--;
                           world[seeker.x][seeker.y - 1] = seeker.symbol;
                           seeker.y--;
                           movable = 0;
                       }
                }
        }
    }
    
    void consume(Animal seeker){
        int i = 0;
        Carnivore prototypeOfWolf;
        Herbivore prototypeOfRabbit;
        if(world[xClosest][yClosest] == 'r')
        {
            for(Herbivore eaten: rabbit)
            {
                if (eaten.x == xClosest && eaten.y == yClosest)
                {
                    death(eaten);
                    prototypeOfWolf = new Carnivore(1, 1);
                    prototypeOfWolf.symbol = 'w';
                    prototypeOfWolf.x = seeker.x;
                    prototypeOfWolf.y = seeker.y;
                    prototypeOfWolf.moveToday = 0;
                    world[seeker.x][seeker.y] = 'w';
                    wolf.add(prototypeOfWolf);
                    break;
                }
            }
        }
        else if(world[xClosest][yClosest] == 177)
        {
            for(Plant eaten: flower)
            {
                if (eaten.x == xClosest && eaten.y == yClosest)
                {
                    death(eaten);
                    prototypeOfRabbit = new Herbivore(1, 2);
                    prototypeOfRabbit.symbol = 'r';
                    prototypeOfRabbit.x = seeker.x;
                    prototypeOfRabbit.y = seeker.y;
                    prototypeOfRabbit.moveToday = 0;
                    world[seeker.x][seeker.y] = 'r';
                    rabbit.add(prototypeOfRabbit);
                    break;
                }
            }
        }
    }
    
    void destroy(int x, int y){
        for(Plant destroyed: flower)
            {
                if (destroyed.x == x && destroyed.y == y)
                {
                    death(destroyed);
                    break;
                }
            }
    }
    
    void death(FormOfLife dying){
        world[dying.x][dying.y] = '.';
        if(dying.symbol == 'w')
        {
            wolf.remove(dying);
        }
        else if(dying.symbol == 'r')
        {
            rabbit.remove(dying);
        }
        else if(dying.symbol == 177)
        {
            flower.remove(dying);
        }
    }
    int reproduction(FormOfLife reproductor){
        Plant prototypeOfFlower;
        int x = 0, y = 0;
        int reproduced = 0;
        int xMode = 0, yMode = 0;
        if(reproductor.x == (size - 1)) xMode = 1;
        else if(reproductor.x == 0) xMode = 2;
        if(reproductor.y == (size - 1)) yMode = 1;
        else if(reproductor.y == 0) yMode = 2;
        if(reproduced == 0 && (xMode != 1))
            if(world[reproductor.x + 1][reproductor.y] == '.'){ x = 1; reproduced = 1;}
        if(reproduced == 0 && (xMode != 2))
            if(world[reproductor.x - 1][reproductor.y] == '.'){ x = -1; reproduced = 1;}
        if(reproduced == 0 && (yMode != 1))
            if(world[reproductor.x][reproductor.y + 1] == '.'){ y = 1; reproduced = 1;}
        if(reproduced == 0 && (yMode != 2))
            if(world[reproductor.x][reproductor.y - 1] == '.'){ y = -1; reproduced = 1;}
        if(reproduced == 0 && xMode != 1 && yMode != 1)    
            if(world[reproductor.x + 1][reproductor.y + 1] == '.'){ x = 1; y = 1; reproduced = 1;}
        if(reproduced == 0 && xMode != 1 && yMode != 2)        
            if(world[reproductor.x + 1][reproductor.y - 1] == '.'){ x = 1; y = -1; reproduced = 1;}
        if(reproduced == 0 && xMode != 2 && yMode != 1)        
            if(world[reproductor.x - 1][reproductor.y + 1] == '.'){ x = -1; y = 1; reproduced = 1;}
        if(reproduced == 0 && xMode != 2 && yMode != 2)        
            if(world[reproductor.x - 1][reproductor.y - 1] == '.'){ x = -1; y = -1; reproduced = 1;}
        if(reproduced == 1)
        {
            prototypeOfFlower = new Plant(1);
            prototypeOfFlower.symbol = 177;
            prototypeOfFlower.x = reproductor.x + x;
            prototypeOfFlower.y = reproductor.y + y;
            prototypeOfFlower.reproductedToday = 1;
            world[prototypeOfFlower.x][prototypeOfFlower.y] = prototypeOfFlower.symbol;
            flower.add(prototypeOfFlower);
        }
        return reproduced;
    }
    
    public void createWolf(){
        int randomed = 0;
        Carnivore prototypeOfWolf;
        randomed = 0;
        prototypeOfWolf = new Carnivore(1, 1);
        prototypeOfWolf.symbol = 'w';
        setPossible();
        if(possible)
        {
            while(randomed == 0)
            {
                prototypeOfWolf.x = randomGenerator.nextInt(size);
                prototypeOfWolf.y = randomGenerator.nextInt(size);
                if(world[prototypeOfWolf.x][prototypeOfWolf.y] == '.')
                {
                world[prototypeOfWolf.x][prototypeOfWolf.y] = prototypeOfWolf.symbol;
                randomed = 1;
                }
            }
            wolf.add(prototypeOfWolf);
        }
    }
    
    public void createRabbit(){
        int randomed = 0;
        Herbivore prototypeOfRabbit;
        randomed = 0;
        prototypeOfRabbit = new Herbivore(1, 2);
        prototypeOfRabbit.symbol = 'r';
        setPossible();
        if(possible)
        {
            while(randomed == 0)
            {
                 prototypeOfRabbit.x = randomGenerator.nextInt(size);
                 prototypeOfRabbit.y = randomGenerator.nextInt(size);
                 if(world[prototypeOfRabbit.x][prototypeOfRabbit.y] == '.')
                 {
                     world[prototypeOfRabbit.x][prototypeOfRabbit.y] = prototypeOfRabbit.symbol;
                     randomed = 1;
                 }
            }
            rabbit.add(prototypeOfRabbit);
        }
    }
    
    public void createFlower(){
        int randomed = 0;
        Plant prototypeOfFlower;
        randomed = 0;
        prototypeOfFlower = new Plant(1);
        prototypeOfFlower.symbol = 177;
        setPossible();
        if(possible)
        {
            while(randomed == 0)
            {
                prototypeOfFlower.x = randomGenerator.nextInt(size);
                prototypeOfFlower.y = randomGenerator.nextInt(size);
                if(world[prototypeOfFlower.x][prototypeOfFlower.y] == '.')
                {
                    world[prototypeOfFlower.x][prototypeOfFlower.y] = prototypeOfFlower.symbol;
                    randomed = 1;
                }
            }
            flower.add(prototypeOfFlower);
        }
    }
    
    public void setPossible(){
        possible = false;
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
                if(world[i][j] == '.')
                {
                    possible = true;
                    break;
                }
            if(possible == true) break;
        }    
    }
    
    private boolean checkIllness(FormOfLife speciment){
        if(speciment instanceof Carnivore)
        {
            if(wolf.size() > (size * size) / 4 || wolf.size() > (rabbit.size() * 2))
                return true;
        }
        else if(speciment instanceof Herbivore)
        {
            if(rabbit.size() > (size * size) / 3 || rabbit.size() > (flower.size() * 2))
                    return true;
        }
        else if(speciment instanceof Plant)
            if(flower.size() > ((size * size) / 3))
                return true;
        return false;
    }
}

