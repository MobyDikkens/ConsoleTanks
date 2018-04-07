/*  Map
 *  
 *  Mapping the map object
 */

namespace ConsoleTanks.Models.Objective
{
    class Map
    {
        public int xLenth { get; private set; }
        public int yLenth { get; private set; }

        private char wall = '*';

        private char[,] map = null;

        public Map(int xLenth, int yLenth, char wall)
        {
            if((xLenth > 0) && (yLenth > 0) && (wall != ' '))
            {
                this.xLenth = xLenth;
                this.yLenth = yLenth;
                this.wall = wall;
                this.map = new char[xLenth, yLenth];
                FillMap();
                FillBounds();
            }
            else
            {
                this.xLenth = 20;
                this.yLenth = 20;
                this.wall = '*';
                this.map = new char[xLenth, yLenth];
                FillMap();
                FillBounds();
            }
        }


        public char[,] GetMap()
        {
            return this.map;
        }

        private void FillBounds()
        {
            //  top and bottom horizontal bounds
            for(int i = 0; i < xLenth; i++)
            {
                map[i, 0] = wall;
                map[i, yLenth - 1] = wall;
            }

            //  left and right vertical bounds
            for(int i = 0; i < yLenth; i++)
            {
                map[0, i] = wall;
                map[xLenth - 1, i] = wall;
            }
        }

        private void FillMap()
        {
            for(int i = 0; i < xLenth; i++)
            {
                for(int j = 0; j < yLenth; j++)
                {
                    map[i, j] = ' ';
                }
            }
        }



    }
}
