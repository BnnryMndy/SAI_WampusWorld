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
                                                
            if ((x < 3) && memory[x + 1, y].isChecked == false && !memory[x + 1, y].mayWampus) memory[x + 1, y].mayWampus = true;
            if ((x > 0) && memory[x - 1, y].isChecked == false && !memory[x - 1, y].mayWampus) memory[x - 1, y].mayWampus = true;
            if ((y > 0) && memory[x, y - 1].isChecked == false && !memory[x, y - 1].mayWampus) memory[x, y - 1].mayWampus = true;
            if ((y < 3) && memory[x, y + 1].isChecked == false && !memory[x, y + 1].mayWampus) memory[x, y + 1].mayWampus = true;
                                                
            if ((x < 3) && memory[x + 1, y].isChecked == true && memory[x + 1, y].mayWampus) memory[x + 1, y].isWampus = true;
            if ((x > 0) && memory[x - 1, y].isChecked == true && memory[x - 1, y].mayWampus) memory[x - 1, y].isWampus = true;
            if ((y > 0) && memory[x, y - 1].isChecked == true && memory[x, y - 1].mayWampus) memory[x, y - 1].isWampus = true;
            if ((y < 3) && memory[x, y + 1].isChecked == true && memory[x, y + 1].mayWampus) memory[x, y + 1].isWampus = true;

        }

        public bool mayDanger(int x, int y)
        {
            return  memory[x, y].isWampus   ||
                    memory[x, y].isHole     ||
                    memory[x, y].mayWampus  ||
                    memory[x, y].mayHole    ||
                    !memory[x, y].isChecked;
        }

        public bool isDanger(int x, int y)
        {
            return memory[x, y].isWampus || memory[x, y].isHole || !memory[x,y].isChecked;
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
                        return true;
                    }
                    
                }
            }
            return false;
            
        }

        public int[] Step()
        {
            //^_____^
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(memory[i,j].isChecked && !memory[i, j].isVisited && !mayDanger(i,j))
                        return new int[]{ i, j };
                }
            }
            //if we reached here, we have some problems
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (memory[i, j].isChecked && !memory[i, j].isVisited && !isDanger(i, j))
                        return new int[] { i, j };
                }
            }
            //(ㆆ_ㆆ) ok
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (memory[i, j].isChecked && !memory[i, j].isVisited)
                        return new int[] { i, j };
                }
            }
            // only for compiler you NEWER reached here
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

        public void GenerateWind(int x, int y)
        {
            if (x < 3) ground[x + 1, y].isWindy = true;
            if (x > 0) ground[x - 1, y].isWindy = true;
            if (y < 3) ground[x, y + 1].isWindy = true;
            if (y > 0) ground[x, y - 1].isWindy = true;
        }


        public void AgentStep()
        {
            bool isPlayerShooted = !agent.canShoot; 
            agent.CheckField(agent.posx, agent.posy);
            if (agent.Shoot())
            {
                foreach (Field field in ground)
                {
                    field.isWampus = false;
                }
            }
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
