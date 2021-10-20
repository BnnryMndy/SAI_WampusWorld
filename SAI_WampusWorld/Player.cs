using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI_WampusWorld
{
    public class Player
    {
        public Field[,] memory = new Field[4, 4]; // память агента
        
        public int score;
        public bool canShoot = true;
        //public int direction = 3;
        public int posx, posy;
        public bool isLive = true;
        public bool isScreamed = false; 
        public bool isWIn = false;

        public bool GetLive() { return isLive; }

        public Player()
        {
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) memory[i, j] = new Field(i, j);
            posx = 0;
            posy = 0;
        }

        public void UpdateField(Field field, int x, int y)
        {
            memory[x, y].isHole = field.isHole;
            memory[x, y].isVisited = field.isVisited;
            memory[x, y].isWampus = field.isWampus;
            memory[x, y].mayHole = field.mayHole;
            memory[x, y].mayWampus = field.mayWampus;
            memory[x, y].isGoldHere = field.isGoldHere;
            memory[x, y].isSmell = field.isSmell;
            memory[x, y].isWindy = field.isWindy;
            memory[x, y].isChecked = field.isChecked;
        }

        public void CheckField(int x, int y)
        {
            
            if (memory[x, y].isWampus || memory[x, y].isHole)
            {
                isLive = false;
                return;
            }

            if (memory[x, y].isGoldHere) Grab();

            if (memory[x, y].isSmell) markWampus(x, y);
            if (memory[x, y].isWindy) markHole(x, y);

            if (!memory[x, y].isSmell && !memory[x, y].isWindy) markSafe(x, y);
            
            memory[x, y].isChecked = true;
            if ((x < 3)) memory[x + 1, y].isChecked = true;
            if ((x > 0)) memory[x - 1, y].isChecked = true;
            if ((y > 0)) memory[x, y - 1].isChecked = true;
            if ((y < 3)) memory[x, y + 1].isChecked = true;
        }

        public void markHole(int x, int y)
        {

            if ((x < 3) && memory[x + 1, y].isChecked == true && !memory[x, y].isSmell) memory[x + 1, y].mayWampus = false;
            if ((x > 0) && memory[x - 1, y].isChecked == true && !memory[x, y].isSmell) memory[x - 1, y].mayWampus = false;
            if ((y > 0) && memory[x, y - 1].isChecked == true && !memory[x, y].isSmell) memory[x, y - 1].mayWampus = false;
            if ((y < 3) && memory[x, y + 1].isChecked == true && !memory[x, y].isSmell) memory[x, y + 1].mayWampus = false;
                                               
            if ((x < 3) && memory[x + 1, y].isChecked == false && !memory[x + 1, y].mayHole) memory[x + 1, y].mayHole = true;
            if ((x > 0) && memory[x - 1, y].isChecked == false && !memory[x - 1, y].mayHole) memory[x - 1, y].mayHole = true;
            if ((y > 0) && memory[x, y - 1].isChecked == false && !memory[x, y - 1].mayHole) memory[x, y - 1].mayHole = true;
            if ((y < 3) && memory[x, y + 1].isChecked == false && !memory[x, y + 1].mayHole) memory[x, y + 1].mayHole = true;
                                               
            if ((x < 3) && memory[x + 1, y].isChecked == true && memory[x + 1, y].mayHole) memory[x + 1, y].isHole = true;
            if ((x > 0) && memory[x - 1, y].isChecked == true && memory[x - 1, y].mayHole) memory[x - 1, y].isHole = true;
            if ((y > 0) && memory[x, y - 1].isChecked == true && memory[x, y - 1].mayHole) memory[x, y - 1].isHole = true;
            if ((y < 3) && memory[x, y + 1].isChecked == true && memory[x, y + 1].mayHole) memory[x, y + 1].isHole = true;

        }

        public void markWampus(int x, int y)
        {
            if ((x < 3) && memory[x + 1, y].isChecked == true && !memory[x, y].isWindy) memory[x + 1, y].mayHole = false;
            if ((x > 0) && memory[x - 1, y].isChecked == true && !memory[x, y].isWindy) memory[x - 1, y].mayHole = false;
            if ((y > 0) && memory[x, y - 1].isChecked == true && !memory[x, y].isWindy) memory[x, y - 1].mayHole = false;
            if ((y < 3) && memory[x, y + 1].isChecked == true && !memory[x, y].isWindy) memory[x, y + 1].mayHole = false;

            if ((x < 3) && !isScreamed /*&& memory[x + 1, y].isChecked == true*/ && memory[x + 1, y].mayWampus) memory[x + 1, y].isWampus = true;
            if ((x > 0) && !isScreamed/*&& memory[x - 1, y].isChecked == true*/ && memory[x - 1, y].mayWampus) memory[x - 1, y].isWampus = true;
            if ((y > 0) && !isScreamed/*&& memory[x, y - 1].isChecked == true*/ && memory[x, y - 1].mayWampus) memory[x, y - 1].isWampus = true;
            if ((y < 3) && !isScreamed/*&& memory[x, y + 1].isChecked == true*/ && memory[x, y + 1].mayWampus) memory[x, y + 1].isWampus = true;

            if ((x < 3) && !isScreamed/* && memory[x + 1, y].isChecked == false */ && !memory[x + 1, y].mayWampus) memory[x + 1, y].mayWampus = true;
            if ((x > 0) && !isScreamed/* && memory[x - 1, y].isChecked == false */ && !memory[x - 1, y].mayWampus) memory[x - 1, y].mayWampus = true;
            if ((y > 0) && !isScreamed/* && memory[x, y - 1].isChecked == false */ && !memory[x, y - 1].mayWampus) memory[x, y - 1].mayWampus = true;
            if ((y < 3) && !isScreamed/* && memory[x, y + 1].isChecked == false */ && !memory[x, y + 1].mayWampus) memory[x, y + 1].mayWampus = true;
                                                
            

        }

        public void markSafe(int x, int y)
        {
            if ((x < 3) ) memory[x + 1, y].mayHole = false;
            if ((x > 0) ) memory[x - 1, y].mayHole = false;
            if ((y > 0) ) memory[x, y - 1].mayHole = false;
            if ((y < 3) ) memory[x, y + 1].mayHole = false;
            if ((x < 3)) memory[x + 1, y].mayWampus = false;
            if ((x > 0)) memory[x - 1, y].mayWampus = false;
            if ((y > 0)) memory[x, y - 1].mayWampus = false;
            if ((y < 3)) memory[x, y + 1].mayWampus = false;
        }

        public bool mayDanger(int x, int y)
        {
            return  (memory[x, y].isWampus && !isScreamed)   ||
                    memory[x, y].isHole     ||
                    (memory[x, y].mayWampus && !isScreamed) ||
                    memory[x, y].mayHole;
        }

        public bool isDanger(int x, int y)
        {
            return (memory[x, y].isWampus && !isScreamed) || memory[x, y].isHole || !memory[x,y].isChecked;
        }

        public void Grab()
        {
            isWIn = true;
        }

        public bool Shoot()
        {
            if (!canShoot) return false;
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(canShoot && memory[i,j].isWampus && (posx == i || posy == j))
                    {
                        memory[i, j].isWampus = false;
                        canShoot = false;
                        isScreamed = true;
                        return true;

                    }
                    
                }
            }
            return false;
            
        }

        public int[] Step()
        {
            //^_____^
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if( (i == 0 ^ j == 0) && (posx + i >= 0 && posx + i < 4 && posy + j >= 0 && posy + j < 4) && memory[posx + i, posy + j].isChecked && !memory[posx + i, posy + j].isVisited && !mayDanger(posx + i, posy + j))
                        return new int[]{ posx + i, posy + j };
                    
                }
            }

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0 ^ j == 0) && (posx + i >= 0 && posx + i < 4 && posy + j >= 0 && posy + j < 4) && memory[posx + i, posy + j].isChecked && !mayDanger(posx + i, posy + j))
                        return new int[] { posx + i, posy + j };

                }
            }

            //if we reached here, we have some problems
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0 ^ j == 0) && (posx + i >= 0 && posx + i < 4 && posy + j >= 0 && posy + j < 4)  && !isDanger(posx + i, posy + j))
                        return new int[] { posx + i, posy + j };
                }
            }
            //(ㆆ_ㆆ) ok
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0 ^ j == 0) && (posx + i >= 0 && posx + i < 4 && posy + j >= 0 && posy + j < 4) && !memory[posx + i, posy + j].isVisited)
                        return new int[] { posx + i, posy + j };
                }
            }
            // only for compiler you NEWER reached here
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0 ^ j == 0) && (posx + i >= 0 && posx + i < 4 && posy + j >= 0 && posy + j < 4))
                        return new int[] { posx + i, posy + j };
                }
            }
            return new int[0];
        }
    }

    public class Field
    {
        public bool isChecked = false;
        public bool isVisited = false;
        public bool isSmell = false;
        public bool isWindy = false;
        public bool isWampus = false;
        public bool isHole = false;
        public bool mayWampus = false;
        public bool mayHole = false;
        public bool isGoldHere = false;
        public bool isPlayerHere = false;
        
        public int x;
        public int y;

        public Field(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            string result = "|";
            if (isPlayerHere) result += "😎";
            if (isVisited) result += "✔"; else result += " ";
            if (isWindy) result += "💨"; else result += " ";
            if (isSmell) result += "👃"; else result += " ";
            if (isHole) result += "🕳"; else result += " ";
            if (isWampus) result += "👹"; else result += " ";
            if (!isHole && mayHole) result += "?🕳";
            if (!isWampus && mayWampus) result += "?👹";
            if (isGoldHere) result += "💰"; else result += " ";
            result += "|";
            return result;
        }
    }

    public class World
    {
        public Field[,] ground = new Field[4, 4];
        public Player agent = new Player();
        public Field Wampus;
        bool isWampusAlive = true;


        public void GenerateMap()
        {
            Random rand = new Random();

            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) ground[i, j] = new Field(i, j);

            int goldX = rand.Next(0, 3);
            int goldy;
            do
            {
               goldy = rand.Next(0, 3);

            } while (goldy == goldX && goldX == 0);
            ground[goldX, goldy].isGoldHere = true;

            goldX = rand.Next(0, 3);
            do
            {
                goldy = rand.Next(0, 3);

            } while (goldy == goldX && goldX == 0);
            ground[goldX, goldy].isWampus = true;
            isWampusAlive = true;
            Wampus = ground[goldX, goldy];
            GenerateSmell(goldX, goldy);

            for (int i = 1; i < 4; i++)
                for (int j = 1; j < 4; j++)
                {
                    int luck = rand.Next(0, 10);
                    if(luck < 2)
                    {
                        ground[i, j].isHole = true;
                        GenerateWind(i, j);
                    }
                }

            agent = new Player();
            ground[0, 0].isPlayerHere = true;
            ground[0, 0].isVisited = true;
            agent.posx = 0;
            agent.posy = 0;
            agent.UpdateField(ground[agent.posx, agent.posy], agent.posx, agent.posy);
        }

        public void GenerateSmell(int x, int y)
        {
            if (x < 3) ground[x + 1, y].isSmell = true;
            if (x > 0) ground[x - 1, y].isSmell = true;
            if (y < 3) ground[x, y + 1].isSmell = true;
            if (y > 0) ground[x, y - 1].isSmell = true;
        }

        void WampusStep()
        {
            if (!isWampusAlive) return;
            if (Wampus.x < 3) ground[Wampus.x + 1, Wampus.y].isSmell = false;
            if (Wampus.x > 0) ground[Wampus.x - 1, Wampus.y].isSmell = false;
            if (Wampus.y < 3) ground[Wampus.x, Wampus.y + 1].isSmell = false;
            if (Wampus.y > 0) ground[Wampus.x, Wampus.y - 1].isSmell = false;

            Random random = new Random();
            int newX, newY, randY;
            int randX = random.Next(-1, +1);
            randY = (randX != 0 ? 0 : random.Next(-1, +1));
            newX = 0;
            newY = 0;

            if (Wampus.x < 3 ) newX = Wampus.x + (randX > 0 ? randX : 0);
            if (Wampus.x > 0 ) newX = Wampus.x + (randX < 0 ? randX : 0);
            if (Wampus.y < 3) newY = Wampus.y + (randY > 0 ? randY : 0);
            if (Wampus.y > 0) newY = Wampus.y + (randY < 0 ? randY : 0);


            ground[Wampus.x, Wampus.y].isWampus = false;
            ground[newX, newY].isWampus = true;
            GenerateSmell(newX, newY);
            Wampus = ground[newX, newY];
        }


        public void GenerateWind(int x, int y)
        {
            if (x < 3) ground[x + 1, y].isWindy = true;
            if (x > 0) ground[x - 1, y].isWindy = true;
            if (y < 3) ground[x, y + 1].isWindy = true;
            if (y > 0) ground[x, y - 1].isWindy = true;
        }


        public void AgentStep()
        {
            WampusStep();
            
            bool isPlayerShooted = !agent.canShoot; 
            agent.CheckField(agent.posx, agent.posy);
            
            if (agent.Shoot())
            {
                foreach (Field field in ground)
                {
                    field.isWampus = false;
                    agent.isScreamed = true;
                    isWampusAlive = false;
                }
            }
            
            //agent.CheckField(agent.posx, agent.posy);
            int[] pos = agent.Step();
            ground[agent.posx, agent.posy].isPlayerHere = false;

            agent.posx = pos[0];
            agent.posy = pos[1];

            ground[agent.posx, agent.posy].isPlayerHere = true;
            ground[agent.posx, agent.posy].isVisited = true;
            agent.UpdateField(ground[agent.posx, agent.posy], agent.posx, agent.posy);
        }
    }
}
