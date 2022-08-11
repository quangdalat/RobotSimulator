using System;
using System.Collections.Generic;
using System.Text;

namespace RobotSimulator
{
    class Common
    {
        public enum Directions
        {
            West,
            South,
            East,
            North
        };

        private readonly static string[] ValidCommands = { "MOVE", "REPORT", "LEFT", "RIGHT", "QUICK" };
        private readonly static string[] ValidDirectionCommands = { "LEFT", "RIGHT" };

        public static string[] ValidDirectionCommand
        {
            get { return ValidDirectionCommands; }   // get method
        }

        public static string[] ValidCommand
        {
            get { return ValidCommands; }   // get method
        }
    }
}
