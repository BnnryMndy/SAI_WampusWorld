using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI_WampusWorld
{
    class Player
    {
        List<field> memory = new List<field>();
        private field[,] ground = new field[4,4];
        private int score;
        private bool canShoot = true;
        private string[] directions = new string[] { "left", "up", "rigth", "down" };
        private int direction = 3;
        private int pos = 11;

        public void turn(int turnDirection)
        {
            direction = (direction + turnDirection) >= 0 ? (directions.Length + turnDirection) : (direction + turnDirection) % directions.Length;
        }

        public void checkFieldStatus(int status)
        {

        }

    }

    class Field
    {
        public bool isChecked = false;
        public bool isSmell = false;
        public bool isWindy = false;
        public bool isWampus = false;
        public bool isHole = false;
        public bool mayWampus = false;
        public bool mayHole = false;
        public bool isGoldHere = false;

        public int x;
        public int y;
    }
}
