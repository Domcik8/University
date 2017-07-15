using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FormsOfLife;
using System.Collections;


/**
* Ecosystem life simulation
*/
namespace Ecosystem
{
    /**
    * @author Dominik Gabriel Lisovski
    * VU MIF PS6
    */
    delegate void LifeTools(Animal life);
    public class World
    {
        Random randomGenerator = new Random();
        int xClosest = 0, yClosest = 0;
        int size = 0;
        char[,] world;
        List<Carnivore> wolf = new List<Carnivore>();
        List<Herbivore> rabbit = new List<Herbivore>();
        List<Plant> flower = new List<Plant>();
        int day = 0;
        bool possible = false;
        
        public World(int size)
        {
            SetSize(size);
            world = new char[size, size];
            CleanWorld();
        }

        void SetSize(int size)
        {
            this.size = size;
            world = new char[size, size];
        }
    
        void CleanWorld()
        {
            int i; int j;
            for(i = 0; i < size; i++)
                for(j = 0; j < size; j++)
                    world[i, j] = '.';
        }
    
        public void CreateAll(int numberOfWolves, int numberOfRabbits, int numberOfFlowers)
        {
            int i;
            for(i = 0; i < numberOfWolves; i++) CreateLife<Carnivore>(new Carnivore());
            for (i = 0; i < numberOfRabbits; i++) CreateLife<Herbivore>(new Herbivore());
            for (i = 0; i < numberOfFlowers; i++) CreateLife<Plant>(new Plant());
        }
    
        public void Println()
        {
            Console.WriteLine("Day: " + day);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++) 
                    Console.Write(world[i, j] + "  ");
                Console.WriteLine();
            }
        }
    
        public void life(int periodOfLife)
        {
            LifeTools Delegate1 = Consume;
            int i, j;
            int found = 0;
            bool dead;
            Carnivore prototypeOfWolf;
            Herbivore prototypeOfRabbit;
            Plant prototypeOfFlower;

            Println();
            for (i = 0; i < periodOfLife; i++)
            {
                try
                {
                    Thread.Sleep(500);
                }
                catch (ArgumentOutOfRangeException exc)
                {
                    System.Console.WriteLine("Error while thread was sleeping");
                    System.Console.WriteLine(exc);
                }
                //Println();
                //Wolfs live
                for(j = 0; j < wolf.Count(); j++)
                {
                    dead = false;
                    prototypeOfWolf = wolf[j];
                    if(prototypeOfWolf.moveToday != 0)
                    {
                        if(prototypeOfWolf.Grow() == 1)
                        {
                                    Death(prototypeOfWolf);
                                    dead = true;
                        }
                        else
                        {
                            found = FindClosestFormOfLife(prototypeOfWolf);
                            if (found == 0) MoveToClosest(Delegate1, prototypeOfWolf);
                        }
                        if(!dead) 
                            if(CheckIllness(prototypeOfWolf)) 
                                if(prototypeOfWolf.Grow() == 1)
                                {
                                        Death(prototypeOfWolf);
                                        dead = true;
                                }
                        if(found == 0 || dead) 
                            {
                                Println();
                                try
                                {
                                    Thread.Sleep(500);
                                }
                                catch (ArgumentOutOfRangeException exc)
                                {
                                    System.Console.WriteLine("Error while thread was sleeping");
                                    System.Console.WriteLine(exc);
                                }
                            }
                    }
                    if(!dead) prototypeOfWolf.moveToday = prototypeOfWolf.speed;
                }
                //println();
                //Rabbits live
                for(j = 0; j < rabbit.Count(); j++)
                {
                    dead = false;
                    prototypeOfRabbit = rabbit[j];
                    if(prototypeOfRabbit.moveToday != 0)
                        {
                            if(prototypeOfRabbit.Grow() == 1)
                            {
                                Death(prototypeOfRabbit);
                                dead = true;
                            }
                            else
                            {
                                found = FindClosestFormOfLife(prototypeOfRabbit);
                                if (found == 0) MoveToClosest(Delegate1, prototypeOfRabbit);
                            }
                            if(!dead) 
                                if(CheckIllness(prototypeOfRabbit)) 
                                    if(prototypeOfRabbit.Grow() == 1)
                                    {
                                            Death(prototypeOfRabbit);
                                            dead = true;
                                    }
                            if(found == 0 || dead) 
                            {
                                Println();
                                try
                                {
                                    Thread.Sleep(500);
                                }
                                catch (ArgumentOutOfRangeException exc)
                                {
                                    System.Console.WriteLine("Error while thread was sleeping");
                                    System.Console.WriteLine(exc);
                                }
                            }
                        }
                    if(!dead) prototypeOfRabbit.moveToday = prototypeOfRabbit.speed;
                }
                //println();
                //Flowers live
                    for(j = 0; j < flower.Count; j++)
                    {
                        dead = false;
                        prototypeOfFlower = flower[j];
                        if(prototypeOfFlower.reproductedToday == 0)
                                if(prototypeOfFlower.Grow() == 1)
                                {
                                    Death(prototypeOfFlower);
                                    dead = true;
                                }
                                else if(!CheckIllness(prototypeOfFlower)) 
                                    prototypeOfFlower.reproductedToday = Reproduction(prototypeOfFlower);
                        if (prototypeOfFlower.reproductedToday > 0 || dead)
                        {
                                Println();
                                try
                                {
                                    Thread.Sleep(500);
                                }
                                catch (ArgumentOutOfRangeException exc)
                                {
                                    System.Console.WriteLine(exc);
                                }
                        }
                        if (!dead) prototypeOfFlower.reproductedToday = 0;
                    }
                    day++;
                    //Println();
            }
        }
    
        int FindClosestFormOfLife(Animal seeker)
        {
            int i = 0;
            double dystans = 0;
            double minDystans = size * size;
            int closestId = -1;
            if(seeker is Carnivore || seeker.symbol == 'w')
            {
                foreach(Herbivore isChecked in rabbit) 
                {
                    dystans = CheckDystans(seeker, isChecked);
                    if(minDystans > dystans) 
                    {
                        minDystans = dystans;
                        closestId = i;
                    }
                    i++;
                }
                if(closestId == -1) return 1;
                xClosest = rabbit[closestId].x;
                yClosest = rabbit[closestId].y;
            }
            if(seeker is Herbivore || seeker.symbol == 'r')
            {
                foreach(Plant isChecked in flower) 
                {
                    dystans = CheckDystans(seeker, isChecked);
                    if(minDystans > dystans) 
                    {
                        minDystans = dystans;
                        closestId = i;
                    }
                    i++;
                }
                if(closestId == -1) return 1;
                xClosest = flower[closestId].x;
                yClosest = flower[closestId].y;
            }  
            return 0;
        }
    
        double CheckDystans(Animal seeker, FormOfLife target)
        {
            return (System.Math.Sqrt((target.x-seeker.x) * (target.x-seeker.x) + (target.y-seeker.y) * (target.y-seeker.y)));
        }
    
        void MoveToClosest(LifeTools consuming, Animal seeker)
        {
            int i, j = 0;
            int movable = 0;
        
            if(seeker.x != xClosest && seeker.moveToday != 0)
            {
                j = seeker.moveToday;
                for(i = 0; i < j; i++)
                    if(seeker.x < xClosest)
                        {
                            if(world[seeker.x + 1, seeker.y] == 'r' && seeker.symbol == 'w') 
                            {
                                seeker.moveToday = 1;
                                consuming(seeker);
                                movable = 1;
                            }
                            else if(world[seeker.x + 1, seeker.y] == 177 && seeker.symbol == 'r') 
                            {
                                seeker.moveToday = 1;
                                consuming(seeker);
                                movable = 1;
                            }
                            else if(world[seeker.x + 1, seeker.y] == 177 && seeker.symbol == 'w')
                            {
                                seeker.moveToday = 1;
                                world[seeker.x, seeker.y] = '.';
                                Destroy(seeker.x + 1, seeker.y);
                                movable = 1;
                            }
                            else if(world[seeker.x + 1, seeker.y] == '.')
                            {
                                world[seeker.x, seeker.y] = '.';
                                movable = 1;
                            }
                            if(movable == 1)
                            {
                                seeker.moveToday--;
                                world[seeker.x + 1, seeker.y] = seeker.symbol;
                                seeker.x++;
                                movable = 0;
                            }
                        }
                j = seeker.moveToday;
                for(i = 0; i < j; i++)
                    if(seeker.x > xClosest)
                    {
                            if(world[seeker.x - 1, seeker.y] == 'r' && seeker.symbol == 'w') 
                            {
                                seeker.moveToday = 1;
                                consuming(seeker);
                                movable = 1;
                            }
                            else if(world[seeker.x - 1, seeker.y] == 177 && seeker.symbol == 'r') 
                            {
                                seeker.moveToday = 1;
                                consuming(seeker);
                                movable = 1;
                            }
                            else if(world[seeker.x - 1, seeker.y] == 177 && seeker.symbol == 'w')
                            {
                                seeker.moveToday = 1;
                                world[seeker.x, seeker.y] = '.';
                                Destroy(seeker.x - 1, seeker.y);
                                movable = 1;
                            }
                            else if(world[seeker.x - 1, seeker.y] == '.')
                            {
                                world[seeker.x, seeker.y] = '.';
                                movable = 1;
                            }
                            if(movable == 1)
                            {
                                seeker.moveToday--;
                                world[seeker.x - 1, seeker.y] = seeker.symbol;
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
                            if(world[seeker.x, seeker.y + 1] == 'r' && seeker.symbol == 'w') 
                            {
                                seeker.moveToday = 1;
                                consuming(seeker);
                                movable = 1;
                            }
                            else if(world[seeker.x, seeker.y + 1] == 177 && seeker.symbol == 'r') 
                            {
                                seeker.moveToday = 1;
                                consuming(seeker);
                                movable = 1;
                            }
                            else if(world[seeker.x, seeker.y + 1] == 177 && seeker.symbol == 'w')
                            {
                                seeker.moveToday = 1;
                                world[seeker.x, seeker.y] = '.';
                                Destroy(seeker.x, seeker.y + 1);
                                movable = 1;
                            }
                            else if(world[seeker.x, seeker.y + 1] == '.')
                            {
                                world[seeker.x, seeker.y] = '.';
                                movable = 1;
                            }
                            if(movable == 1)
                            {
                                seeker.moveToday--;
                                world[seeker.x, seeker.y + 1] = seeker.symbol;
                                seeker.y++;
                                movable = 0;
                            }
                    }
                j = seeker.moveToday;
                for(i = 0; i < j; i++)
                    if(seeker.y > yClosest)
                    {
                            if(world[seeker.x, seeker.y - 1] == 'r' && seeker.symbol == 'w') 
                            {
                                seeker.moveToday = 1;
                                consuming(seeker);
                                movable = 1;
                            }
                            else if(world[seeker.x, seeker.y - 1] == 177 && seeker.symbol == 'r') 
                            {
                                seeker.moveToday = 1;
                                consuming(seeker);
                                movable = 1;
                            }
                            else if(world[seeker.x, seeker.y - 1] == 177 && seeker.symbol == 'w')
                            {
                                seeker.moveToday = 1;
                                world[seeker.x, seeker.y] = '.';
                                Destroy(seeker.x, seeker.y - 1);
                                movable = 1;
                            }
                            else if(world[seeker.x, seeker.y - 1] == '.')
                            {
                                world[seeker.x, seeker.y] = '.';
                                movable = 1;
                            }
                            if(movable == 1)
                            {
                                seeker.moveToday--;
                                world[seeker.x, seeker.y - 1] = seeker.symbol;
                                seeker.y--;
                                movable = 0;
                            }
                    }
            }
        }
    
        void Consume(Animal seeker)
        {
            Carnivore prototypeOfWolf;
            Herbivore prototypeOfRabbit;
            if(world[xClosest, yClosest] == 'r')
            {
                foreach(Herbivore eaten in rabbit)
                {
                    if (eaten.x == xClosest && eaten.y == yClosest)
                    {
                        Death(eaten);
                        prototypeOfWolf = new Carnivore(1, 1);
                        prototypeOfWolf.symbol = 'w';
                        prototypeOfWolf.x = seeker.x;
                        prototypeOfWolf.y = seeker.y;
                        prototypeOfWolf.moveToday = 0;
                        world[seeker.x, seeker.y] = 'w';
                        wolf.Add(prototypeOfWolf);
                        break;
                    }
                }
            }
            else if(world[xClosest, yClosest] == 177)
            {
                foreach(Plant eaten in flower)
                {
                    if (eaten.x == xClosest && eaten.y == yClosest)
                    {
                        Death(eaten);
                        prototypeOfRabbit = new Herbivore(1, 2);
                        prototypeOfRabbit.symbol = 'r';
                        prototypeOfRabbit.x = seeker.x;
                        prototypeOfRabbit.y = seeker.y;
                        prototypeOfRabbit.moveToday = 0;
                        world[seeker.x, seeker.y] = 'r';
                        rabbit.Add(prototypeOfRabbit);
                        break;
                    }
                }
            }
        }
    
        void Destroy(int x, int y)
        {
            foreach(Plant destroyed in flower)
                {
                    if (destroyed.x == x && destroyed.y == y)
                    {
                        Death(destroyed);
                        break;
                    }
                }
        }
    
        void Death(FormOfLife dying)
        {
            world[dying.x, dying.y] = '.';
            if(dying.symbol == 'w')
            {
                wolf.Remove((Carnivore)dying);
            }
            else if(dying.symbol == 'r')
            {
                rabbit.Remove((Herbivore)dying);
            }
            else if(dying.symbol == 177)
            {
                flower.Remove((Plant)dying);
            }
        }
        int Reproduction(FormOfLife reproductor)
        {
            Plant prototypeOfFlower;
            int x = 0, y = 0;
            int reproduced = 0;
            int xMode = 0, yMode = 0;
            if(reproductor.x == (size - 1)) xMode = 1;
            else if(reproductor.x == 0) xMode = 2;
            if(reproductor.y == (size - 1)) yMode = 1;
            else if(reproductor.y == 0) yMode = 2;
            if(reproduced == 0 && (xMode != 1))
                if(world[reproductor.x + 1, reproductor.y] == '.'){ x = 1; reproduced = 1;}
            if(reproduced == 0 && (xMode != 2))
                if(world[reproductor.x - 1, reproductor.y] == '.'){ x = -1; reproduced = 1;}
            if(reproduced == 0 && (yMode != 1))
                if(world[reproductor.x, reproductor.y + 1] == '.'){ y = 1; reproduced = 1;}
            if(reproduced == 0 && (yMode != 2))
                if(world[reproductor.x, reproductor.y - 1] == '.'){ y = -1; reproduced = 1;}
            if(reproduced == 0 && xMode != 1 && yMode != 1)    
                if(world[reproductor.x + 1, reproductor.y + 1] == '.'){ x = 1; y = 1; reproduced = 1;}
            if(reproduced == 0 && xMode != 1 && yMode != 2)        
                if(world[reproductor.x + 1, reproductor.y - 1] == '.'){ x = 1; y = -1; reproduced = 1;}
            if(reproduced == 0 && xMode != 2 && yMode != 1)        
                if(world[reproductor.x - 1, reproductor.y + 1] == '.'){ x = -1; y = 1; reproduced = 1;}
            if(reproduced == 0 && xMode != 2 && yMode != 2)        
                if(world[reproductor.x - 1, reproductor.y - 1] == '.'){ x = -1; y = -1; reproduced = 1;}
            if(reproduced == 1)
            {
                prototypeOfFlower = new Plant(1);
                prototypeOfFlower.symbol = Convert.ToChar(177);
                prototypeOfFlower.x = reproductor.x + x;
                prototypeOfFlower.y = reproductor.y + y;
                prototypeOfFlower.reproductedToday = -1;
                world[prototypeOfFlower.x, prototypeOfFlower.y] = prototypeOfFlower.symbol;
                flower.Add(prototypeOfFlower);
            }
            return reproduced;
        }


        public void CreateLife<T>(T formOfLife)
        {
            FormOfLife prototype;
            if(formOfLife is Carnivore)
            {
                prototype = new Carnivore(1, 1);
                prototype.symbol = 'w';
            }
            else if(formOfLife is Herbivore)
            {
                prototype = new Herbivore(1, 2);
                prototype.symbol = 'r';
            }
            else
            {
                prototype = new Plant(1);
                prototype.symbol = Convert.ToChar(177);
            }
            int randomed = 0;
            SetPossible();
            if(possible)
            {
                while(randomed == 0)
                {
                    prototype.x = randomGenerator.Next(size);
                    prototype.y = randomGenerator.Next(size);
                    if (world[prototype.x, prototype.y] == '.')
                    {
                        world[prototype.x, prototype.y] = prototype.symbol;
                        randomed = 1;
                    }
                    if (formOfLife is Carnivore)
                    {
                        wolf.Add((Carnivore)prototype);
                    }
                    else if (formOfLife is Herbivore)
                    {
                        rabbit.Add((Herbivore)prototype);
                    }
                    else
                    {
                        flower.Add((Plant)prototype);
                    }
                }

            }
        }


        /**
        public void CreateWolf()
        {
            int randomed = 0;
            Carnivore prototypeOfWolf;
            prototypeOfWolf = new Carnivore(1, 1);
            prototypeOfWolf.symbol = 'w';
            SetPossible();
            if(possible)
            {
                while(randomed == 0)
                {
                    prototypeOfWolf.x = randomGenerator.Next(size);
                    prototypeOfWolf.y = randomGenerator.Next(size);
                    if(world[prototypeOfWolf.x, prototypeOfWolf.y] == '.')
                    {
                    world[prototypeOfWolf.x, prototypeOfWolf.y] = prototypeOfWolf.symbol;
                    randomed = 1;
                    }
                }
                wolf.Add(prototypeOfWolf);
            }
        }
    
        public void CreateRabbit()
        {
            int randomed = 0;
            Herbivore prototypeOfRabbit;
            prototypeOfRabbit = new Herbivore(1, 2);
            prototypeOfRabbit.symbol = 'r';
            SetPossible();
            if(possible)
            {
                while(randomed == 0)
                {
                        prototypeOfRabbit.x = randomGenerator.Next(size);
                        prototypeOfRabbit.y = randomGenerator.Next(size);
                        if(world[prototypeOfRabbit.x, prototypeOfRabbit.y] == '.')
                        {
                            world[prototypeOfRabbit.x, prototypeOfRabbit.y] = prototypeOfRabbit.symbol;
                            randomed = 1;
                        }
                }
                rabbit.Add(prototypeOfRabbit);
            }
        }
    
        public void CreateFlower()
        {
            int randomed = 0;
            Plant prototypeOfFlower;
            prototypeOfFlower = new Plant(1);
            prototypeOfFlower.symbol = Convert.ToChar(177);
            SetPossible();
            if(possible)
            {
                while(randomed == 0)
                {
                    prototypeOfFlower.x = randomGenerator.Next(size);
                    prototypeOfFlower.y = randomGenerator.Next(size);
                    if(world[prototypeOfFlower.x, prototypeOfFlower.y] == '.')
                    {
                        world[prototypeOfFlower.x, prototypeOfFlower.y] = prototypeOfFlower.symbol;
                        randomed = 1;
                    }
                }
                flower.Add(prototypeOfFlower);
            }
        }
         */

        public void SetPossible()
        {
            possible = false;
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                    if(world[i, j] == '.')
                    {
                        possible = true;
                        break;
                    }
                if(possible == true) break;
            }    
        }
    
        private bool CheckIllness(FormOfLife speciment)
        {
            if(speciment is Carnivore)
            {
                if(wolf.Count() > (size * size) / 4 || wolf.Count() > (rabbit.Count() * 2))
                    return true;
            }
            else if(speciment is Herbivore)
            {
                if(rabbit.Count() > (size * size) / 3 || rabbit.Count() > (flower.Count() * 2))
                        return true;
            }
            else if(speciment is Plant)
                if(flower.Count() > ((size * size) / 3))
                    return true;
            return false;
        }
    }
}
