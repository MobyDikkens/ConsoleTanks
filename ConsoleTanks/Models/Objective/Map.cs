/*  Map
 *  
 *  Mapping the map object
 */

using System;
using System.Collections.Generic;

namespace ConsoleTanks.Models.Objective
{
    class Map
    {
        public int xLenth { get; private set; }
        public int yLenth { get; private set; }

        private char wall = '*';

        private char[,] map = null;

        private ConsoleColor color;

        public Map(int xLenth, int yLenth, char wall, ConsoleColor color)
        {
            if((xLenth > 0) && (yLenth > 0) && (wall != ' '))
            {
                this.xLenth = xLenth;
                this.yLenth = yLenth;
                this.wall = wall;
                this.map = new char[xLenth, yLenth];
                this.color = color;
                FillMap();
                FillBounds();
                //BuildLabirint();
            }
            else
            {
                this.xLenth = 20;
                this.yLenth = 20;
                this.wall = '*';
                this.map = new char[xLenth, yLenth];
                this.color = color;
                FillMap();
                FillBounds();
                //BuildLabirint();
            }
        }


        public char[,] GetMap()
        {
            return this.map;
        }

        public ConsoleColor GetColor()
        {
            return this.color;
        }

        public bool IsEmpty(int x, int y)
        {
            if((x < xLenth) && (x >= 0)
                && (y < yLenth) && (y >= 0))
            {
                if(map[x,y] == ' ')
                {
                    return true;
                }
            }

            return false;
        }

        public void BuildLabirint()
        {
            //  visited vertexes
            List<Position> visited = new List<Position>();

            int generalCount = (xLenth - 1) * (yLenth - 1);

            Position current = new Position();
            current.X = 1;
            current.Y = 1;

            Position left = new Position();
            Position right = new Position();
            Position up = new Position();
            Position down = new Position();

            Stack<Position> stack = new Stack<Position>();

            while(visited.Count < generalCount / 2)
            {
                //  surrounding vertexes
                left.X = current.X - 1;
                left.Y = current.Y;

                right.X = current.X + 1;
                right.Y = current.Y;

                down.X = current.X;
                down.Y = current.Y - 1;

                up.X = current.X;
                up.Y = current.Y + 1;

                //  find not visited surrounding
                if(!NotVisited(visited, left))
                {
                    stack.Push(current);
                    visited.Add(current);
                    current = left;
                }
                else
                {
                    if(!NotVisited(visited, right))
                    {
                        stack.Push(current);
                        visited.Add(current);
                        current = right;
                    }
                    else
                    {
                        if(!NotVisited(visited, up))
                        {
                            stack.Push(current);
                            visited.Add(current);
                            current = up;
                        }
                        else
                        {
                            if(!NotVisited(visited, down))
                            {
                                stack.Push(current);
                                visited.Add(current);
                                current = down;
                            }
                        }
                    }
                }
            

            }

            foreach (var tmp in stack)
            {
                map[tmp.X, tmp.Y] = wall;
            }


        }


        private bool NotVisited(List<Position> visited, Position position)
        {
            if (position.X >= 0 && position.Y >= 0 && position.X < xLenth && position.Y < yLenth)
            {
                if (!visited.Contains(position))
                    return false;
            }

            return true;
        }



        #region Helpers

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

        #endregion

        #region Structures

        private struct Position
        {
            public int X;
            public int Y;
        }

        #endregion

    }
}
