/*  Visualizer
 *  
 *  Helps to render a map
 *  and objects
 */

namespace ConsoleTanks.Rendering.Objective
{
    class Visualizer : IVisualization
    {
        private char[,] visualMap = null;

        public int xLenth { get; private set; }
        public int yLenth { get; private set; }

        public void RenderMap(char[,] map, int xLenth, int yLenth)
        {
            this.visualMap = map;
            this.xLenth = xLenth;
            this.yLenth = yLenth;
        }

        public void RenderObject(char skin, int x, int y)
        {
            if((x < xLenth) && (x >= 0) && (y < yLenth) && (y >= 0))
            {
                this.visualMap[x, y] = skin;
            }
        }

        public void Visualize()
        {
            for(int i = 0; i < xLenth; i++)
            {
                for(int j = 0; j < yLenth; j++)
                {
                    System.Console.Write(visualMap[i,j]);
                }
                System.Console.WriteLine();
            }
            Clear();
        }

        private void Clear()
        {
            this.visualMap = null;
            this.xLenth = 0;
            this.yLenth = 0;
        }

    }
}
