/*  IVisualization
 *  
 *  Provide functionallity need
 *  to implements an object to could be
 *  visualizer
 */

namespace ConsoleTanks.Rendering
{
    interface IVisualization
    {
        void RenderMap(char[,] map, int xLenth, int yLenth);
        void RenderObject(char skin, int x, int y);
    }
}
