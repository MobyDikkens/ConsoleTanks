using ConsoleTanks.Models.Objective;
using System;

namespace ConsoleTanks
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(-20, 20, '*');

            var m = map.GetMap();
            for(int i = 0; i < map.xLenth; i++)
            {
                for(int j = 0; j < map.yLenth; j++)
                {
                    Console.Write(m[i,j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
