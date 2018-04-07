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

        public char[,] GetVisualization()
        {
            return this.visualMap;
        }

        public void Clear()
        {
            this.visualMap = null;
        }

    }
}
