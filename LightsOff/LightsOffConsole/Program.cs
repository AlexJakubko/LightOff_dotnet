using System;
using LightsOffCore.Core;

namespace LightsOff.LightsOffConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            int level = 0;
            Console.WriteLine("Please insert your name.");
            Console.Write("Insert your name here:");
            string playersName = Console.ReadLine()?.Trim();
            while (true)
            {
                while (true)
                {
                    Console.WriteLine("Please insert levels difficulty (1 to 10)for exit insert X ");
                    Console.Write("Insert level here:");
                    var input = Console.ReadLine()?.Trim().ToUpper();
                    if ("X" == input)
                        Environment.Exit(0);

                    bool result = int.TryParse(input, out level);
                    if ((level > 0) && (level <= 10))
                    {
                        break;
                    }
                    else if (result == true)
                    {
                        Console.WriteLine("You are out of range ! Try again.");
                    }
                    else
                    {
                        Console.WriteLine("Wrong input ! Try again.");
                    }
                }
                var field = new Field(5, 5, level,playersName);
                var ui = new ConsoleUi(field);
                ui.Play();

                Console.WriteLine("Do you like to play again or end the game ? Insert: Play or End");
                while (true)
                {
                    Console.Write("Answer:");
                    var input = Console.ReadLine()?.Trim().ToUpper();
                    if ("PLAY" == input)
                    {
                        break;
                    }
                    else if ("END" == input)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Wrong input! Try again! ");
                    }
                }
            }
        }
    } 
}