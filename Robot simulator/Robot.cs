using System;
using System.Collections.Generic;
using System.Text;
using static RobotSimulator.Common;

namespace RobotSimulator
{
    class Robot
    {
       
        private int posX, posY;
        private Directions direction;

        public Robot()
        {
            this.posX = 0;
            this.posY = 0;
            this.direction = Directions.North;
        }

        public int PosX   // property
        {
            get { return posX; }   // get method
            set { posX = value; }  // set method
        }

        public int PosY   // property
        {
            get { return posY; }   // get method
            set { posY = value; }  // set method
        }

        public Directions Direction   // property
        {
            get { return direction; }   // get method
            set { direction = value; }  // set method
        }

        
    }
}
