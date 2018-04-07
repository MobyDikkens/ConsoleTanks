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
               
        private int GUID
        {
            get
            {
                this.guid++;
                return this.guid;
            }
        }

        //  <tank id, tank on map>
        private Dictionary<int, TankOnMap> tanksCollection = null;

        //  <bullet id, bullet>
        private Dictionary<int, Bullet> bulletsCollection = null;



        public GameController(int xLenth, int yLenth, char wall)
        {
            if ((xLenth > 0) && (yLenth > 0) && (wall != ' '))
            {
                this.tanksCollection = new Dictionary<int, TankOnMap>();
                this.map = new Map(xLenth, yLenth, wall);
            }
            else
            {
                this.tanksCollection = new Dictionary<int, TankOnMap>();
                this.map = new Map(20, 20, '*');
            }
        }

        public bool AddTank(int damage, char bulletSkin, int hp, char skin)
        {
            if((bulletSkin != ' ') && (hp > 0) && (skin != ' '))
            {
                TankOnMap newTank = new TankOnMap();
                int guid = this.GUID;
                newTank.Tank = new Tank(guid, damage, bulletSkin, hp, skin);


                //  find an empty place on the map
                //
                //
                bool founded = false;// indecates if the position was found


                for(int i = 0; i < map.xLenth; i++)
                {
                    //  to stop if the position already founded
                    if(founded)
                    {
                        break;
                    }
                    for(int j = 0; j < map.yLenth; j++)
                    {
                        if(map.IsEmpty(i,j))
                        {
                            newTank.Position.X = i;
                            newTank.Position.Y = j;
                            founded = true;
                            break;
                        }
                    }
                }

                //  add to the collection
                tanksCollection.Add(guid, newTank);

                return true;
            }
            else
            {
                return false;
            }
        }


        

        #region Inside Stuctures

        //  each one of tanks have its own position
        struct Position
        {
            public int X;
            public int Y;
        }

        struct TankOnMap
        {
            public Tank Tank;
            public Position Position;
        }

        #endregion

    }

}
