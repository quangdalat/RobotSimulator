using System;
using System.Collections.Generic;
using System.Text;

namespace RobotSimulator
{
    class SquareTable
    {
        private Robot robot;
        private readonly int units = 0;
        public int[][] tableSize;

        public Robot MyRobot   // property
        {
            get { return robot; }   // get method
            set { robot = value; }  // set method
        }

        public int TableSize{
            get { return units; }
        }

        public SquareTable(int size)
        {
            if (size > 0)
            {
                units = size;
                tableSize = new int[size][];
                for (int i = 0; i < size; i++)
                {
                    tableSize[i] = new int[size];
                }
            }
            
        }

        public bool CheckValidPos(int x, int y){
            if (x >= 0 && x < this.TableSize && y >= 0 && y < this.TableSize)
            {
                return true;
            }
            return false;
        }
    }
}
