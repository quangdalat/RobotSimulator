using System;
namespace RobotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string command;
                bool loop = true;
                Action action = new Action(5);
                while (loop)
                {
                    command = Console.ReadLine();
                    command = action.CleanUpCommand(command);
                    if (action.CheckCmdValid(command))
                    {
                        action.HandleCommand(command);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
