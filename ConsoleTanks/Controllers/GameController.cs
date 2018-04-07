/*  GameControlle
 *  
 *  Main brains of the game
 */

using ConsoleTanks.Models.Objective;
using ConsoleTanks.Models.Abstract;
using ConsoleTanks.Rendering.Objective;
using System.Collections.Generic;

namespace ConsoleTanks.Controllers
{
    class GameController
    {
        private Map map = null;

        //  to identify each one of tanks
        private int guid = 0;

        //  <tank id, tanks on map>
        private Dictionary<int, TankOnMap> tanksCollection = null;




        //  each one of tanks have its own position
        struct Position
        {
            public int X;
            public int Y;
        }

        struct TankOnMap
        {
            Tank tank;
            Position position;
        }

    }

}
