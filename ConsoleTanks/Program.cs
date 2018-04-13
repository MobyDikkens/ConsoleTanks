using ConsoleTanks.Models.Objective;
using System;

namespace ConsoleTanks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 30;
            Console.WindowWidth = 150;
            
            ConsoleTanks.Controllers.GameController gameController = new Controllers.GameController(30, 100, '*');
            gameController.AddPlayer(5, '5', 100, '^', System.ConsoleKey.S, System.ConsoleKey.W, System.ConsoleKey.A, System.ConsoleKey.D, System.ConsoleKey.Spacebar);
            gameController.Start();
            System.Threading.Thread.Sleep(-1);
            Console.ReadKey();
        }
    }
}
