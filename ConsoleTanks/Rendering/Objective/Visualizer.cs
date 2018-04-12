/*  Visualizer
 *  
 *  Helps to render a map
 *  and objects
 */

namespace ConsoleTanks.Rendering.Objective
{
    static class Visualizer
    {
        public static void Render(char[,] value, int xLenth, int yLenth, System.ConsoleColor color)
        {
            System.Console.SetCursorPosition(0, 0);
            for(int i = 0; i < xLenth; i++)
            {
                for(int j = 0; j < yLenth; j++)
                {
                    System.Console.ForegroundColor = color;
                    System.Console.Write(value[i, j]);
                }
                System.Console.WriteLine();
            }
            System.Console.ResetColor();
        }

        public static void Render(char value,int x,int y,System.ConsoleColor color)
        {
            System.Console.SetCursorPosition(x, y);
            System.Console.ForegroundColor = color;
            System.Console.Write(value);
            System.Console.ResetColor();
        }

    }
}
