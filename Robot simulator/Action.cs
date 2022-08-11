using System;
using System.Collections.Generic;
using System.Text;
using static RobotSimulator.Common;

namespace RobotSimulator
{
    class Action
    {
        private readonly SquareTable myTable;
        private readonly Robot myRobot;
        private readonly int tableSize;
        public Action(int tableSize)
        {
            this.tableSize = tableSize;
            this.myTable ??= new SquareTable(tableSize);
            this.myRobot ??= new Robot();
            this.myTable.MyRobot = this.myRobot;
        }

        public string Place(int x, int y, Directions direction)
        {
            string error = "";
            if (this.myTable.CheckValidPos(x, y) && this.myTable.MyRobot != null)
            {
                this.myTable.MyRobot.PosX = x;
                this.myTable.MyRobot.PosY = y;
                this.myTable.MyRobot.Direction = direction;
            }
            else
                error = "Invalid place";
            return error;
        }

        public string Move()
        {
            string error = "";
            bool hasError = false;
            //string[] cmd = !string.IsNullOrEmpty(moveCmd) ? moveCmd.Split(',') : new string[0];
            if (this.myTable.MyRobot != null && this.tableSize > 0)
            {
                switch (this.myTable.MyRobot.Direction)
                {
                    case Common.Directions.West:
                        if (this.myTable.CheckValidPos(this.myTable.MyRobot.PosX, this.myTable.MyRobot.PosY - 1))
                        {
                            this.myTable.MyRobot.PosY = this.myTable.MyRobot.PosY - 1;
                        }
                        else
                            hasError = true;

                        break;
                    case Common.Directions.South:
                        if (this.myTable.CheckValidPos(this.myTable.MyRobot.PosX - 1, this.myTable.MyRobot.PosY))
                        {
                            this.myTable.MyRobot.PosX = this.myTable.MyRobot.PosX - 1;
                        }
                        else
                            hasError = true;

                        break;
                    case Common.Directions.East:
                        if (this.myTable.CheckValidPos(this.myTable.MyRobot.PosX, this.myTable.MyRobot.PosY + 1))
                        {
                            this.myTable.MyRobot.PosY = this.myTable.MyRobot.PosY + 1;
                        }
                        else
                            hasError = true;

                        break;
                    case Common.Directions.North:
                        if (this.myTable.CheckValidPos(this.myTable.MyRobot.PosX + 1, this.myTable.MyRobot.PosY))
                        {
                            this.myTable.MyRobot.PosX = this.myTable.MyRobot.PosX + 1;
                        }
                        else
                            hasError = true;

                        break;
                }

                error = hasError ? "Invalid move" : "";
            }
            return error;
        }

        public void Report()
        {
            Console.WriteLine(this.myTable.MyRobot.PosY.ToString() + "," + this.myTable.MyRobot.PosX.ToString()  + "," + this.myTable.MyRobot.Direction.ToString().ToUpper());
        }

        public string CleanUpCommand(string cmd)
        {
            if (!string.IsNullOrEmpty(cmd))
            { 
                while(cmd.IndexOf("  ") >= 0)
                {
                    cmd = cmd.Replace("  ", " ");
                }
            }
            return cmd;
        }

        public void Turn(string cmd)
        {

            if (Array.Exists(Common.ValidDirectionCommand, item => item.Equals(cmd.ToUpper().Trim(' '))))
            {
                switch (this.myTable.MyRobot.Direction)
                {
                    case Common.Directions.West:
                        this.myTable.MyRobot.Direction = Common.ValidDirectionCommand[0] == cmd.ToUpper().Trim(' ') ? Directions.South : Directions.North;
                        break;
                    case Common.Directions.South:
                        this.myTable.MyRobot.Direction = Common.ValidDirectionCommand[0] == cmd.ToUpper().Trim(' ') ? Directions.East : Directions.West;
                        break;
                    case Common.Directions.East:
                        this.myTable.MyRobot.Direction = Common.ValidDirectionCommand[0] == cmd.ToUpper().Trim(' ') ? Directions.North : Directions.South;
                        break;
                    case Common.Directions.North:
                        this.myTable.MyRobot.Direction = Common.ValidDirectionCommand[0] == cmd.ToUpper().Trim(' ') ? Directions.West : Directions.East;
                        break;
                }
            }
        }

        public void HandleCommand(string cmd)
        {
            if (!string.IsNullOrEmpty(cmd) && cmd.ToUpper().IndexOf("PLACE ") >= 0)
            {
                string[] cmdArr = ParserPlaceCommand(cmd);
                if (cmdArr.Length == 3)
                {
                    Int32.TryParse(cmdArr[0], out int y);
                    Int32.TryParse(cmdArr[1], out int x);
                    Enum.TryParse(cmdArr[2].ToUpper().Trim(' '), out Directions myDirection);
                    Place(x, y, myDirection);
                }
            }
            else
            {
                if (cmd.ToUpper().Trim(' ') == Common.ValidCommand[0])
                {
                    this.Move();
                }
                else if (cmd.ToUpper().Trim(' ') == Common.ValidCommand[1])
                {
                    this.Report();
                }
                else if (cmd.ToUpper().Trim(' ') == Common.ValidCommand[2] || cmd.ToUpper().Trim(' ') == Common.ValidCommand[3])
                {
                    this.Turn(cmd.ToUpper().Trim(' '));
                }
                else if (cmd.ToUpper().Trim(' ') == Common.ValidCommand[4])
                {
                    Environment.Exit(0);
                }
                    
            }
        }

        public bool CheckCmdValid(string cmd)
        {
            if (!string.IsNullOrEmpty(cmd) && cmd.ToUpper().IndexOf("PLACE ") >= 0)
            {
                string[] cmdArr = ParserPlaceCommand(cmd);
                if (cmdArr.Length == 3)
                {
                    Int32.TryParse(cmdArr[0], out int y);
                    Int32.TryParse(cmdArr[1], out int x);
                    return x >= 0 && y >= 0 && Array.Exists(Common.ValidDirectionCommand, item => item.Equals(cmdArr[2].ToUpper().Trim(' ')));
                }
            }
            else
            {
                return Array.Exists(Common.ValidCommand, item => item.Equals(cmd.ToUpper().Trim(' ')));
            }
            return false;
        }

        private string[] ParserPlaceCommand(string cmd)
        {
            cmd = cmd.ToUpper().Replace("PLACE ", "");
            return cmd.Split(',');
        }
    }
}
